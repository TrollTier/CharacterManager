using System.Text;
using System.Data.SQLite; 

namespace CharacterManager.DbUpdates
{
  class Update_1_0_0 : IDbUpdate
  {
    public int NextMajor
    {
      get { return 1; }
    }

    public int NextMinor
    {
      get { return 1; }
    }

    public int NextRevision
    {
      get { return 0; }
    }


    /// <summary>
    /// Executes the commands that are neccessary for database version 1.0.0
    /// </summary>
    /// <param name="con">The opened connection to the database.</param>
    public void Execute(SQLiteConnection con)
    {
      //Table factions
      StringBuilder cmd = new StringBuilder();
      cmd.Append("CREATE TABLE IF NOT EXISTS Factions (");
      cmd.Append("ID TEXT PRIMARY KEY NOT NULL"); 
      cmd.Append(", Name TEXT"); 
      cmd.Append(", Description TEXT");
      cmd.Append(", ImagePath Text"); 
      cmd.AppendLine(");"); 

      //Table character factions
      cmd.Append("CREATE TABLE IF NOT EXISTS CharacterFactions (");
      cmd.Append("CharacterId TEXT NOT NULL");
      cmd.Append(", FactionId TEXT NOT NULL");
      cmd.Append(", PRIMARY KEY(CharacterID, FactionId)");
      cmd.Append(", FOREIGN KEY(CharacterID) REFERENCES Characters(ID)");
      cmd.Append(", FOREIGN KEY(FactionId) REFERENCES Factions(ID)"); 
      cmd.Append(");");

      new SQLiteCommand(cmd.ToString(), con).ExecuteNonQuery(); 
    }
  }
}
