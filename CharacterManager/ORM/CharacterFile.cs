using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
  public class CharacterFile
  {
    public string ID { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Path { get; set; }

    public String CharacterID { get; set; }

    public virtual Character Character { get; set; }
  }
}
