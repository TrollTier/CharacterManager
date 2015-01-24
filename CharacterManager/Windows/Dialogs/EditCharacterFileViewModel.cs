using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarkSunProgramming.Windows.Dialogs
{
  class EditCharacterFileViewModel
  {
    public string FileName
    {
      get;
      set; 
    }

    public string FileDescription
    {
      get;
      set; 
    }

    public EditCharacterFileViewModel()
    {
      
    }

    public EditCharacterFileViewModel(string fileName, string description) : this()
    {
      FileName = fileName;
      FileDescription = description; 
    }
  }
}
