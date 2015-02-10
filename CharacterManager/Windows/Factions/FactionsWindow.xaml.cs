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

namespace CharacterManager.Windows
{
  /// <summary>
  /// Interaction logic for FactionsWindow.xaml
  /// </summary>
  public partial class FactionsWindow : Window
  {
    public FactionsWindow()
    {
      InitializeComponent();
      DataContext = new FactionsWindowVM(); 
    }

    private void imgFactionImage_MouseDown(object sender, MouseButtonEventArgs e)
    {
      ((FactionsWindowVM)DataContext).SetImage(); 
    }
  }
}
