using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
  [Table("DatabaseInfo")]
  public class DatabaseInfo
  {
    [Key]
    public long MajorVersion { get; set; }
    public int MinorVersion { get; set; }
    public int Revision { get; set; }
  }
}
