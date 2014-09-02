using DarkSunProgramming.Languages;
using DarkSunProgramming.Options;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CharacterManager.Windows
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private MainVM context; 

    public MainWindow()
    {
      try
      {
        AppSettings.LoadOptions("CharacterManager.ini");
        LanguageService.Load(String.Format(".\\Languages\\{0}.txt", AppSettings.GetOption("General", "Language")), ":=:");
        DataContext = context = new MainVM();

        InitializeComponent();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }

    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (context != null)
        context.SetImage(); 
    }
  }
}
