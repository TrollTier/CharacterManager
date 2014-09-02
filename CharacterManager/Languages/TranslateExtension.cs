using DarkSunProgramming.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DarkSunProgramming.Languages
{
  class TranslateExtension : MarkupExtension
  {

    [ConstructorArgument("key")]
    public string TranslationKey { get; set; }

    public TranslateExtension()
    {

    }

    public TranslateExtension(string key)
    {
      TranslationKey = key; 
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      try
      {
        return LanguageService.GetString(TranslationKey); 
      }
      catch { return ""; }
    }
  }
}
