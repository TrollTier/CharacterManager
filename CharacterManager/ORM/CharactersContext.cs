using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
  public class CharactersContext : DbContext
  {
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterFile> CharacterFiles { get; set; }
    public DbSet<DatabaseInfo> DatabaseInfos { get; set; }
    public DbSet<Faction> Factions { get; set; }

    public CharactersContext() : base("name=CharactersContext")
    {
      Database.SetInitializer<CharactersContext>(null); 
    }
  }
}
