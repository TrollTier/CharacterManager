using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CharacterManager.Windows
{
  class ViewModelBase<T> : DependencyObject, INotifyPropertyChanged
  {
    #region properties

    /// <summary>
    /// The database context to use for queries.
    /// </summary>
    public static readonly DependencyProperty ContextProperty = DependencyProperty.Register(
      "Context", typeof(CharactersContext), typeof(ViewModelBase<T>));

    /// <summary>
    /// The currently selected object.
    /// </summary>
    public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register(
      "SelectedObject", typeof(T), typeof(ViewModelBase<T>),
      new PropertyMetadata(new PropertyChangedCallback(OnSelectedObjectChanged)));

    /// <summary>
    /// Gets the database context to use.
    /// </summary>
    public CharactersContext Context
    {
      get { return (CharactersContext)GetValue(ContextProperty); }
      protected set { SetValue(ContextProperty, value); }
    }

    /// <summary>
    /// Gets or sets the currently selected object.
    /// </summary>
    public T SelectedObject
    {
      get { return (T)GetValue(SelectedObjectProperty); }
      set { SetValue(SelectedObjectProperty, value); }
    }

    #endregion

    private static void OnSelectedObjectChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      ((ViewModelBase<T>)sender).OnSelectedObjectChanged(); 
    }

    /// <summary>
    /// Wird aufgerufen, wenn das aktuell ausgewählte Objekt geändert wurde. 
    /// Muss nicht von Kind-Klassen aufgerufen werden.
    /// </summary>
    protected virtual void OnSelectedObjectChanged() { }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(object sender, string propName)
    {
      if (PropertyChanged != null)
        PropertyChanged(sender, new PropertyChangedEventArgs(propName)); 
    }

    protected virtual void OnPropertyChanged(string propName)
    {
      OnPropertyChanged(this, propName);
    }
  }
}
