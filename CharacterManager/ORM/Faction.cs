using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
  /// <summary>
  /// An instance of this class represents a faction. 
  /// A faction is an organization that has characters as members 
  /// and it's own interests.
  /// </summary>
  [Table("Factions")]
  public class Faction : NotifierObject 
  {

#region fields

    [Key]
    private string id; 
    private string name; 
    private string description; 
    private string imagePath; 

#endregion

#region properties

    /// <summary>
    /// Gets the id of this faction.
    /// </summary>
    public string ID 
    { 
      get {return id;} 
      private set { id = value;}
    }

    /// <summary>
    /// Gets or sets the name of this faction.
    /// </summary>
    public string Name 
    {
      get { return name; } 
      set 
      { 
        name = value; 
        OnPropertyChanged("Name"); 
      }
    }

    /// <summary>
    /// Gets or sets the description of this faction.
    /// </summary>
    public string Description 
    { 
      get { return description; } 
      set 
      { 
        description =value; 
        OnPropertyChanged("Description"); 
      }
    }

    /// <summary>
    /// Gets or sets the image path of this faction.
    /// </summary>
    public string ImagePath
    {
      get { return imagePath; }
      set 
      {
        imagePath = value; 
        OnPropertyChanged("ImagePath");
      }
    }

    /// <summary>
    /// Gets the Characters of this faction.
    /// </summary>
    public ICollection<Character> Characters { get; protected set; }    
#endregion

    /// <summary>
    /// Creates a new instance of the <code>Faction</code> class.
    /// </summary>
    public Faction()
    {
      id = Guid.NewGuid().ToString(); 
    }
  }
}
