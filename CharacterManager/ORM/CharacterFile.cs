using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterManager
{
  public class CharacterFile : NotifierObject
  {
    private string id;
    private string name;
    private string description;
    private string path; 

    public string ID 
    {
      get { return id;  }
      set
      {
        id = value;
        OnPropertyChanged("ID"); 
      }
    }

    public string Name 
    {
      get { return name;  }
      set { name = value; OnPropertyChanged("Name"); } 
    }

    public string Description 
    {
      get { return description;  }
      set { description = value; OnPropertyChanged("Description"); } 
    }

    public string Path 
    {
      get { return path; }
      set { path = value; OnPropertyChanged("Path"); }
    }

    public String CharacterID { get; set; }

    public virtual Character Character { get; set; }
  }
}
