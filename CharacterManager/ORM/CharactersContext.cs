using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
  public class CharactersContext : DbContext
  {
    public DbSet<Character> CharacterSet { get; set; }
    public DbSet<CharacterFile> CharacterFileSet { get; set; }

    public CharactersContext()
    {
      Database.SetInitializer<CharactersContext>(null); 
    }
  }
}
