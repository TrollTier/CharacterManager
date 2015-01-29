using DarkSunProgramming.Commands;
using DarkSunProgramming.InteractionServices;
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
    public IEnumerable<Faction> Factions
    {
      get
      {
        List<Faction> db = Context.Factions.ToList();
        List<Faction> local = Context.Factions.Local.Where((x) => Context.Entry<Faction>(x).State == System.Data.Entity.EntityState.Added).ToList();

        return db.Union(local);  
      }
    }

    public bool IsEditable
    {
      get { return SelectedObject != null; }
    }

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
      }
      catch (Exception ex)
      {
        UserInteractionService.ShowError(ex.ToString(), "Error"); 
      }      
    }

    #endregion

    protected override void OnSelectedObjectChanged()
    {
      OnPropertyChanged("IsEditable");
    }
  }
}
