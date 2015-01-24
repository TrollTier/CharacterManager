using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
  [Table("CharacterFactions")]
  public class CharacterFaction
  {
    [Key]
    private string characterID;
    [Key]
    private string factionID;

    public string CharacterID
    {
      get {return characterID; }
      private set {characterID = value;}
    }

    public string FactionID
    {
      get  { return factionID;}
      private set { factionID = value; }
    }

    public ICollection<Character> Characters {get; set;}
    public ICollection<Faction> Factions {get; set;}
  }
}
