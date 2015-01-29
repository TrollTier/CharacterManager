using CharacterManager.UserControls;
using DarkSunProgramming.Commands;
using DarkSunProgramming.InteractionServices;
using DarkSunProgramming.Languages;
using DarkSunProgramming.Windows.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

    /// <summary>
    /// Gets the availale characters
    /// </summary>
    public IEnumerable<Character> Characters
    {
      get
      {
        IEnumerable<Character> chars = (from c in Context.Characters
                                        select c);

        chars.OrderBy((x) => x.Name);
        return chars.ToList<Character>(); 
      }
    }

    /// <summary>
    /// Gets the image of the currently selected character.
    /// </summary>
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
    
    /// <summary>
    /// Gets a boolean value, indicating wether the character can be edited.
    /// </summary>
    public bool IsEditingEnabled
    {
      get { return Convert.ToBoolean(GetValue(IsEditingEnabledProperty)); }
      private set { SetValue(IsEditingEnabledProperty, value); }
    }
    
    /// <summary>
    /// Gets a boolean value, indicating wether a file can be edited or not.
    /// </summary>
    public bool IsEditingFileEnabled
    {
      get { return Convert.ToBoolean(GetValue(IsEditingFileEnabledProperty)); }
      set { SetValue(IsEditingFileEnabledProperty, value); }
    }
    
    /// <summary>
    /// Gets the available character files.
    /// </summary>
    public IEnumerable<CharacterFile> CharacterFiles
    {
      get 
      {
        if (SelectedObject == null) return null;

        return (from file in SelectedObject.CharacterFiles
                select file).OrderBy((x) => x.Name).ToList();
      }
    }

    /// <summary>
    /// Gets or sets the currently selected file.
    /// </summary>
    public CharacterFile SelectedFile
    {
      get { return (CharacterFile)GetValue(SelectedFileProperty); }
      set { SetValue(SelectedFileProperty, value); }
    }
    
    #endregion

    #region properties

    private ObservableCollection<CharacterFileElement> characterFileElements =
      new ObservableCollection<CharacterFileElement>();

    /// <summary>
    /// Gets the character file elements of the currently selected character.
    /// </summary>
    public ObservableCollection<CharacterFileElement> CharacterFileElements { get { return characterFileElements; } }

    #endregion

    public MainVM() 
    {
      try
      {
        Context = new CharactersContext();
        InitializeCommands();
      }
      catch(Exception ex)
      {
        throw new Exception(String.Format("Verbindung zur Datenquelle konnte nicht hergestellt werden.{0}", ex.Message));
      }
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
      EditFileCommand = new DelegatedCommand(new Action(EditFile), new Func<bool>(CanEditFile));
      ManageFactionsCommand = new DelegatedCommand(() => ManageFactions(), new Func<bool>(CanManageFactions));
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

        Context.Characters.Add(chr);
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
        Context.Characters.Remove((Character)SelectedObject);
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
        string filePath = UserInteractionService.GetFilePath();

        if (!String.IsNullOrWhiteSpace(filePath))
        {
          CharacterFile file = new CharacterFile();
          file.ID = Guid.NewGuid().ToString();
          file.Name = "Neue Datei";
          file.CharacterID = SelectedObject.ID;
          file.Path = filePath;

          SelectedObject.CharacterFiles.Add(file);
          Context.SaveChanges();

          OnPropertyChanged("CharacterFiles");
          CreateNewFileElement(file);
          SelectedFile = file;
        }
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

        CharacterFileElement element = characterFileElements.Where((x) => x.File == SelectedFile).FirstOrDefault();
        if (element != null)
          characterFileElements.Remove(element); 

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


    public ICommand EditFileCommand { get; private set; }

    public bool CanEditFile()
    {
      return SelectedFile != null; 
    }

    public void EditFile()
    {
      EditCharacterFileDialog win = new EditCharacterFileDialog(SelectedFile.Name, SelectedFile.Description);
      win.ShowDialog(); 

      if (win.DialogResult != null && win.DialogResult.Value)
      {
        SelectedFile.Name = win.FileName;
        SelectedFile.Description = win.Description; 
      }
    }


    public ICommand ManageFactionsCommand { get; private set; }

    public bool CanManageFactions() { return true; } 

    public void ManageFactions()
    {
      FactionsWindow win = new FactionsWindow();
      win.ShowDialog(); 
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


      CreateCharacterFileElements();
      OnPropertyChanged("CharacterFiles"); 
    }

    private void CreateCharacterFileElements()
    {
      characterFileElements.Clear(); 

      if (CharacterFiles != null)
      {
        foreach (CharacterFile file in CharacterFiles)
        {
          CreateNewFileElement(file);
        }
      }
    }

    private void CreateNewFileElement(CharacterFile file)
    {
      if (file != null)
      {
        CharacterFileElement element;
        element = new CharacterFileElement(file, new BitmapImage(new Uri("pack://application:,,,/Resources/icon_pdf.png")));
        element.PreviewMouseDown += FileElement_MouseDown;

        element.ContextMenu = CreateFileElementContextMenu(); 
        characterFileElements.Add(element);
      }
    }

    private ContextMenu CreateFileElementContextMenu()
    {
      ContextMenu menu = new ContextMenu();
      
      MenuItem item = new MenuItem()
      {
        Header = LanguageService.GetString("000018"), 
        Command = EditFileCommand,
        Icon = new System.Windows.Controls.Image()
        {
          Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Pencil-icon.png")),
          Width=25, 
          Height = 25
        }
      };
      menu.Items.Add(item); 


      item = new MenuItem(); 
      item.Header = LanguageService.GetString("000015"); 
      item.Command = OpenFileCommand;
      item.Icon = new System.Windows.Controls.Image()
      {
        Source = new BitmapImage(new Uri("pack://application:,,,/Resources/open.png")), 
        Width = 25, 
        Height = 25
      };
      menu.Items.Add(item);

      item = new MenuItem();
      item.Header = LanguageService.GetString("000008");
      item.Command = DeleteFileCommand;
      item.Icon = new System.Windows.Controls.Image()
      {
        Source = new BitmapImage(new Uri("pack://application:,,,/Resources/delete_1.png")),
        Width = 25,
        Height = 25
      }; 
      menu.Items.Add(item);

      item = new MenuItem();
      item.Header = LanguageService.GetString("000016");
      item.Command = ChangeFileCommand;
      item.Icon = new System.Windows.Controls.Image()
      {
        Source = new BitmapImage(new Uri("pack://application:,,,/Resources/open.png")), 
        Width = 25, 
        Height = 25
      };
      menu.Items.Add(item); 

      return menu; 

    }

    private void FileElement_MouseDown(object sender, MouseButtonEventArgs e)
    {
      CharacterFileElement element = (CharacterFileElement)sender;
      SelectedFile = element.File; 
    }

    private static void OnSelectedFileChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      MainVM send = (MainVM)sender;
      send.IsEditingFileEnabled = send.SelectedFile != null; 
    }
  }
}
