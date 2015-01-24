using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite; 

namespace CharacterManager.DbUpdates
{
  /// <summary>
  /// This class's purpose is to update the database (preferably on application start up).
  /// It uses the version info in the DatabaseInfo table.
  /// </summary>
  public class DbUpdates
  {
    //Update these fields after a new update routine has been implemented.
    //Revision = small changes (extra fields, column changes, etc.).
    //Minor = Bigger changes, like a new table. 
    //Major = Multiple bigger updates (i.e. for a new feature).
    private const int CurrentMajor = 1;
    private const int CurrentMinor = 1;
    private const int CurrentRevision = 0; 

    public void UpdateDatabase(string dbPath)
    {
      try
      {
        CreateDatabaseInfoIfNeccessary(dbPath);   

        using (CharactersContext ctx = new CharactersContext())
        {
          DatabaseInfo info = ctx.DatabaseInfos.First();
          UpdateDatabase(info, ctx, dbPath); 
        }
      }
      catch (Exception ex)
      {
        throw ex; 
      }
    }

    private void CreateDatabaseInfoIfNeccessary(string dbPath)
    {
      StringBuilder cmd = new StringBuilder();
      cmd.Append("CREATE TABLE IF NOT EXISTS DatabaseInfo (");
      cmd.Append("MajorVersion INTEGER NOT NULL PRIMARY KEY");
      cmd.Append(", MinorVersion INTEGER NOT NULL");
      cmd.Append(", Revision INTEGER NOT NULL");
      cmd.AppendLine(");"); 

      using (SQLiteConnection con = new SQLiteConnection(String.Format("Data Source={0}", dbPath)))
      {
        con.Open();

        SQLiteCommand command = new SQLiteCommand(cmd.ToString(), con);
        command.ExecuteNonQuery(); 
      }; 
    
      using (CharactersContext ctx = new CharactersContext())
      {
        DatabaseInfo dbInfo = ctx.DatabaseInfos.FirstOrDefault(); 
        if (dbInfo == null)
        {
          dbInfo = new DatabaseInfo(); 
          dbInfo.MajorVersion = 1;
          dbInfo.MinorVersion = 0;
          dbInfo.Revision = 0;

          ctx.DatabaseInfos.Add(dbInfo);
          ctx.SaveChanges(); 
        }
      }
    }

    private void UpdateDatabase(DatabaseInfo info, CharactersContext dbContext, string dbPath)
    {
      using (SQLiteConnection con = new SQLiteConnection(String.Format("Data Source={0}", dbPath)))
      {
        con.Open();

        if (info.MajorVersion == 1 && info.MinorVersion == 0 && info.Revision == 0)
          new Update_1_0_0().Execute(con);

        con.Close(); 
      }
      
      info.MajorVersion = CurrentMajor;
      info.MinorVersion = CurrentMinor;
      info.Revision = CurrentRevision;
      dbContext.SaveChanges(); 
    }

    private void DoUpdate(DatabaseInfo dbInfo, SQLiteConnection con, IDbUpdate update)
    {
      update.Execute(con);
      dbInfo.MajorVersion = update.NextMajor;
      dbInfo.MinorVersion = update.NextMinor;
      dbInfo.Revision = update.NextRevision; 
    }
  }
}
