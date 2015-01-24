using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DarkSunProgramming.Windows.Dialogs
{
  /// <summary>
  /// Interaction logic for EditCharacterFileDialog.xaml
  /// </summary>
  public partial class EditCharacterFileDialog : Window
  {
    private EditCharacterFileViewModel context; 

    public string FileName
    {
      get { return context.FileName;  }
    }

    public string Description
    {
      get { return context.FileDescription; }
    }

    public EditCharacterFileDialog()
    {
      InitializeComponent();
      DataContext = context = new EditCharacterFileViewModel();
    }

    /// <summary>
    /// Initializes a new instance of the EditCharacterFileDialog class.
    /// </summary>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="description">The description of the file.</param>
    public EditCharacterFileDialog(string fileName, string description)
    {
      InitializeComponent();
      DataContext = context = new EditCharacterFileViewModel(fileName, description);
    }

    private void cmdCancel_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
      Close(); 
    }

    private void cmdSave_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
      Close(); 
    }
  }
}
