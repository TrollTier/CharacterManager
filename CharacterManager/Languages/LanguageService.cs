using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DarkSunProgramming.Languages
{
  static class LanguageService
  {
    /// <summary>
    /// The available strings for the loaded language.
    /// </summary>
    private static Dictionary<string, string> Strings
      = new Dictionary<string, string>(); 


    /// <summary>
    /// Gets the string with the specified key.
    /// <para>
    /// Return null, if the key is not in the dictionary.
    /// </para>
    /// </summary>
    /// <param name="key">The key of the string to get.</param>
    public static string GetString(string key)
    {
      string value;

      if (Strings.TryGetValue(key, out value))
        return value; 
      else
        return String.Empty; 

    }

    /// <summary>
    /// Loads the globalization strings from the specified file.
    /// </summary>
    /// <param name="file">The file to load.</param>
    /// <param name="separator">The string that separates keys from values.</param>
    public static void Load(string file, string separator)
    {
      try
      {
        StreamReader sr = new StreamReader(file);
        string currentKey = "";
        string currentString = "";

        string line;
        int indexOfSeparator; 

        while (!sr.EndOfStream)
        {
          line = sr.ReadLine();
          indexOfSeparator = line.IndexOf(separator); 

          if(indexOfSeparator > -1)
          {
            if(!string.IsNullOrWhiteSpace(currentKey))
              Strings.Add(currentKey, currentString);

            currentKey = line.TrimStart().Substring(0, indexOfSeparator).TrimEnd();
            currentString = line.TrimStart().Substring(indexOfSeparator + separator.Length, line.Length - (indexOfSeparator + separator.Length)).TrimStart(); 
          }
          else
          {
            currentString += line; 
          }
        }

        if (!Strings.ContainsKey(currentKey))
          Strings.Add(currentKey, currentString); 
      }
      catch(Exception ex)
      {
        throw ex; 
      }
    }
  }
}
