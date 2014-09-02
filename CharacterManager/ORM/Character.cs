using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
  public class Character
  {
    private ICollection<CharacterFile> characterFiles; 

    public String ID { get; set; }
    public String Name { get; set; }
    public String Description { get; set; }
    public String ImagePath { get; set; }

    /// <summary>
    /// true = male, false = female. (No offense!)
    /// </summary>
    public bool Gender { get; set; } 

    public virtual ICollection<CharacterFile> CharacterFiles 
    { 
      get { return characterFiles ?? (characterFiles = new Collection<CharacterFile>()); }
      protected set { characterFiles = value; }
    } 


  }
}
