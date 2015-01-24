using System.Data.SQLite; 

namespace CharacterManager.DbUpdates
{
  interface IDbUpdate
  {
    /// <summary>
    /// The next major version after this update.
    /// </summary>
    int NextMajor { get; }

    /// <summary>
    /// The next minor version after this update.
    /// </summary>
    int NextMinor { get; }

    /// <summary>
    /// The next revision after this update.
    /// </summary>
    int NextRevision { get; }

    void Execute(SQLiteConnection con);
  }
}
