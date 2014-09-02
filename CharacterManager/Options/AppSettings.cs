using DarkSunProgramming.IniFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSunProgramming.Options
{
  static class AppSettings
  {
    private static IniWrapper ini; 
    
    public static String GetOption(string category, string key)
    {
      return ini.GetValue(category, key); 
    }

    public static void SetOption(string category, string key, string value)
    {
      if (ini == null)
        throw new Exception("Options haven't been loaded yet.");

      ini.SetValue(category, key, value); 
    }

    public static void LoadOptions(string file)
    {
      try
      {
        ini = new IniWrapper();
        ini.LoadIniFile(file); 
      }
      catch(Exception ex)
      {
        throw ex; 
      }
    }

    public static void SaveOptions()
    {
      if (ini == null)
        throw new Exception("Options haven't been loaded yet.");

      ini.Save(); 
    }
  }
}
