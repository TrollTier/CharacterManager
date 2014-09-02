using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using forms = System.Windows.Forms; 

namespace DarkSunProgramming.InteractionServices
{
  public class UserInteractionService
  {
    public static string GetFilePath(string initialDirectory = null)
    {
      forms.OpenFileDialog ofd = new forms.OpenFileDialog();

      if (initialDirectory == null)
        ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
      else
        ofd.InitialDirectory = initialDirectory;

      ofd.Multiselect = false;
      ofd.ShowDialog();

      return ofd.FileName;       
    }

    public static void ShowMessage(string message, string caption)
    {
      MessageBox.Show(message, caption); 
    }

    public static void ShowError(string message, string caption)
    {
      MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }
  }
}
