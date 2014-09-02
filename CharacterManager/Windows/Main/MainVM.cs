using DarkSunProgramming.Commands;
using DarkSunProgramming.IniFiles;
using DarkSunProgramming.InteractionServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CharacterManager.Windows
{
  class MainVM : ViewModelBase<Character>
  {
    #region dependency properties

    public static readonly DependencyProperty IsEditingEnabledProperty = DependencyProperty.Register(
      "IsEditingEnabled", typeof(bool), typeof(MainVM));

    public static readonly DependencyProperty IsEditingFileEnabledProperty = DependencyProperty.Register(
      "IsEditingFileEnabled", typeof(bool), typeof(MainVM));

    public static readonly DependencyProperty SelectedFileProperty = DependencyProperty.Register(
      "SelectedFile", typeof(CharacterFile), typeof(MainVM), new PropertyMetadata(new PropertyChangedCallback(OnSelectedFileChanged)));

    #endregion

    #region property wrappers

    public IEnumerable<Character> Characters
    {
      get
      {
        IEnumerable<Character> chars = (from c in Context.CharacterSet
                                        select c);

        chars.OrderBy((x) => x.Name);
        return chars.ToList<Character>(); 
      }
    }

    public ImageSource CharacterImage
    {
      get
      {
        if(!(SelectedObject == null || string.IsNullOrEmpty(SelectedObject.ImagePath)))
        {
          try
          {
            return new BitmapImage(new Uri(SelectedObject.ImagePath));
          }
          catch(Exception ex)
          {
            UserInteractionService.ShowMessage(string.Format("Bild konnte nicht geladen werden gefunden werden: \r\nPfad: {0}\r\nFehler: {1}",
              SelectedObject.ImagePath, ex.Message), "Fehler beim Laden des Bildes.");

            if (SelectedObject.Gender)
              return new BitmapImage(new Uri("pack://application:,,,/Resources/dummy_male.png"));
            else
              return new BitmapImage(new Uri("pack://application:,,,/Resources/dummy_female.png")); 
          }
        }

        return new BitmapImage(new Uri("pack://application:,,,/Resources/dummy.png")); 
      }
    }
    
    public bool IsEditingEnabled
    {
      get { return Convert.ToBoolean(GetValue(IsEditingEnabledProperty)); }
      private set { SetValue(IsEditingEnabledProperty, value); }
    }
    
    public bool IsEditingFileEnabled
    {
      get { return Convert.ToBoolean(GetValue(IsEditingFileEnabledProperty)); }
      set { SetValue(IsEditingFileEnabledProperty, value); }
    }
    
    public IEnumerable<CharacterFile> CharacterFiles
    {
      get 
      {
        if (SelectedObject == null) return null;

        return (from file in SelectedObject.CharacterFiles
                select file).OrderBy((x) => x.Name).ToList();
      }
    }

    public CharacterFile SelectedFile
    {
      get { return (CharacterFile)GetValue(SelectedFileProperty); }
      set { SetValue(SelectedFileProperty, value); }
    }
    
    #endregion

    public MainVM() 
    {
      try
      {
        Context = new CharactersContext();
      }
      catch(Exception ex)
      {
        throw new Exception(String.Format("Verbindung zur Datenquelle konnte nicht hergestellt werden.{0}", ex.Message));
      }

      InitializeCommands();
    }

    #region Commands

    private void InitializeCommands()
    {
      CreateCharacterCommand = new DelegatedCommand(new Action(CreateCharacter), new Func<bool>(CanCreateCharacter));
      DeleteCharacterCommand = new DelegatedCommand(new Action(DeleteCharacter), new Func<bool>(CanDeleteCharacter));
      SaveCommand = new DelegatedCommand(new Action(Save), new Func<bool>(CanSave));
      CreateFileCommand = new DelegatedCommand(new Action(CreateFile), new Func<bool>(CanCreateFile));
      DeleteFileCommand = new DelegatedCommand(new Action(DeleteFile), new Func<bool>(CanDeleteFile));
      ChangeFileCommand = new DelegatedCommand(new Action(ChangeFile), new Func<bool>(CanChangeFile));
      OpenFileCommand = new DelegatedCommand(new Action(OpenFile), new Func<bool>(CanOpenFile));
    }
     
    public ICommand CreateCharacterCommand { get; private set; }
    
    private bool CanCreateCharacter()
    {
      return Context != null; 
    }
    
    private void CreateCharacter()
    {
      if (CanCreateCharacter())
      {
        Character chr = new Character();
        chr.ID = Guid.NewGuid().ToString();
        chr.Name = "Neuer Character";

        Context.CharacterSet.Add(chr);
        Context.SaveChanges();
        
        OnPropertyChanged("Characters");
        SelectedObject = chr;
      }
    }

    
    public ICommand DeleteCharacterCommand { get; private set; }

    public bool CanDeleteCharacter()
    {
      return SelectedObject != null; 
    }

    public void DeleteCharacter()
    {
      if(CanDeleteCharacter())
      {
        Context.CharacterSet.Remove((Character)SelectedObject);
        Context.SaveChanges();

        OnPropertyChanged("Characters");
        SelectedObject = null; 
      }
    }


    public ICommand SaveCommand { get; private set; }

    public bool CanSave()
    {
      return Context != null; 
    }

    public void Save()
    {
      if(CanSave())
      {
        try
        {
          Context.SaveChanges();
          UserInteractionService.ShowMessage("Speichern erfolgreich", "Erfolg"); 
        }
        catch(Exception ex)
        {
          UserInteractionService.ShowError(String.Format("Fehler beim Speichern aufgetreten.{0}", ex.Message), 
            "Fehler aufgetreten"); 
        }
      }
    }


    public ICommand CreateFileCommand { get; private set; }

    public bool CanCreateFile()
    {
      return SelectedObject != null;
    }

    public void CreateFile()
    {
      if(CanCreateFile())
      {
        CharacterFile file = new CharacterFile();
        file.ID = Guid.NewGuid().ToString();
        file.Name = "Neue Datei";
        file.CharacterID = SelectedObject.ID; 

        file.Path = UserInteractionService.GetFilePath();

        SelectedObject.CharacterFiles.Add(file);
        Context.SaveChanges();

        OnPropertyChanged("CharacterFiles");
        SelectedFile = file; 
      }
    }


    public ICommand DeleteFileCommand { get; private set; }

    public bool CanDeleteFile()
    {
      return SelectedFile != null && SelectedObject != null;
    }

    public void DeleteFile()
    {
      if(CanDeleteFile())
      {
        SelectedObject.CharacterFiles.Remove(SelectedFile);
        Context.SaveChanges();

        OnPropertyChanged("CharacterFiles");
        SelectedFile = null;
      }
    }


    public ICommand ChangeFileCommand { get; private set; }

    public bool CanChangeFile()
    {
      return SelectedFile != null; 
    }

    public void ChangeFile()
    {
      if(CanChangeFile())
      {
        FileInfo fi = new FileInfo(SelectedFile.Path); 
        string init = (string.IsNullOrWhiteSpace(fi.FullName) ? null : fi.DirectoryName);

        string path = UserInteractionService.GetFilePath(init); 
        if(!string.IsNullOrWhiteSpace(path))
        {
          SelectedFile.Path = path;
          OnPropertyChanged(SelectedFile, "Path");
        }
      }
    }


    public ICommand OpenFileCommand { get; private set; }

    public bool CanOpenFile()
    {
      return SelectedFile != null && !String.IsNullOrWhiteSpace(SelectedFile.Path);
    }

    public void OpenFile()
    {
      if(CanOpenFile())
      {
        Process p = new Process();
        p.StartInfo = new ProcessStartInfo(SelectedFile.Path);
        p.Start();
      }
    }

    #endregion

    public void SetImage()
    {
      if (SelectedObject == null) return; 

      string path = UserInteractionService.GetFilePath();
      if(!string.IsNullOrWhiteSpace(path))
      {
        SelectedObject.ImagePath = path;
        OnPropertyChanged("CharacterImage"); 
      }
    }

    protected override void OnSelectedObjectChanged()
    {
      IsEditingEnabled = SelectedObject != null;
      SelectedFile = null;
      OnPropertyChanged("CharacterImage");
      OnPropertyChanged("CharacterFiles"); 
    }

    private static void OnSelectedFileChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      MainVM send = (MainVM)sender;
      send.IsEditingFileEnabled = send.SelectedFile != null; 
    }
  }
}
