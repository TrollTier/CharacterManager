using CharacterManager;
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

namespace CharacterManager.UserControls
{
  /// <summary>
  /// Interaction logic for CharacterFileElement.xaml
  /// </summary>
  public partial class CharacterFileElement : UserControl
  {
    /// <summary>
    /// The displayed icon.
    /// </summary>
    public static DependencyProperty IconProperty = DependencyProperty.Register(
      "Icon", typeof(ImageSource), typeof(CharacterFileElement));

    /// <summary>
    /// The name of the file to display.
    /// </summary>
    public static DependencyProperty FileProperty = DependencyProperty.Register(
      "File", typeof(CharacterFile), typeof(CharacterFileElement));

    /// <summary>
    /// Gets or sets the displayed file.
    /// </summary>
    public CharacterFile File
    {
      get { return (CharacterFile)GetValue(FileProperty); }
      set { SetValue(FileProperty, value); }
    }

    /// <summary>
    /// Gets or sets the displayed icon.
    /// </summary>
    public ImageSource Icon
    {
      get { return (ImageSource)GetValue(IconProperty); }
      set { SetValue(IconProperty, value); }
    }

    public CharacterFileElement(CharacterFile file, ImageSource icon)
    {
      InitializeComponent();
      DataContext = this;

      File = file; 
      Icon = icon; 
    }
  }
}
