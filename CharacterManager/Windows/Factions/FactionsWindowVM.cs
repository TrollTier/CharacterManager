using DarkSunProgramming.Commands;
using DarkSunProgramming.InteractionServices;
using DarkSunProgramming.Languages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CharacterManager.Windows
{
  class FactionsWindowVM : ViewModelBase<Faction>
  {
    /// <summary>
    /// Gets the available factions.
    /// </summary>
    public IEnumerable<Faction> Factions
    {
      get
      {
        List<Faction> db = Context.Factions.ToList();
        List<Faction> local = Context.Factions.Local.Where((x) => Context.Entry<Faction>(x).State == System.Data.Entity.EntityState.Added).ToList();

        return db.Union(local);  
      }
    }

    /// <summary>
    /// Gets a boolean value, indicating wether the selcted faction can be edited or not.
    /// </summary>
    public bool IsEditable
    {
      get { return SelectedObject != null; }
    }

    /// <summary>
    /// Gets the image for the selected faction.
    /// </summary>
    public BitmapImage FactionImage
    {
      get 
      {
        if (SelectedObject == null || String.IsNullOrWhiteSpace(((Faction)SelectedObject).ImagePath))
        {
          return new BitmapImage(new Uri("pack://application:,,,/Resources/factions.png")); 
        }
        else
        {
          return new BitmapImage(new Uri(((Faction)SelectedObject).ImagePath));
        }
      }
    }


    public FactionsWindowVM()
    {
      Context = new CharactersContext();
      InitializeCommands(); 
    }

    #region commands

    private void InitializeCommands()
    {
      AddCommand = new DelegatedCommand(() => AddFaction(), () => CanAdd());
      SaveCommand = new DelegatedCommand(() => Save(), () => CanSave()); 
    }
    
    public ICommand AddCommand
    {
      get;
      private set;
    }

    public bool CanAdd()
    {
      return true; 
    }

    public void AddFaction()
    {
      try
      {
        Faction f = new Faction();
        f.Name = DarkSunProgramming.Languages.LanguageService.GetString("000024");

        Context.Factions.Add(f);
        OnPropertyChanged("Factions");

        SelectedObject = f;
      }
      catch (Exception ex)
      {
        UserInteractionService.ShowError(ex.ToString(), "Error"); 
      }      
    }

    public ICommand SaveCommand { get; private set; }

    public bool CanSave() { return true;  }

    public void Save()
    {
      try
      {
        Context.SaveChanges();
        UserInteractionService.ShowMessage(LanguageService.GetString("000026"), LanguageService.GetString("000027")); 
      }
      catch (Exception ex)
      {
        UserInteractionService.ShowError(ex.Message, LanguageService.GetString("000025")); 
      }
    }

    #endregion

    public void SetImage()
    {
      if (SelectedObject == null) return;

      string path = UserInteractionService.GetFilePath();
      if (!string.IsNullOrWhiteSpace(path))
      {
        SelectedObject.ImagePath = path;
        OnPropertyChanged("FactionImage");
      }
    }

    protected override void OnSelectedObjectChanged()
    {
      OnPropertyChanged("IsEditable");
      OnPropertyChanged("FactionImage"); 
    }
  }
}
