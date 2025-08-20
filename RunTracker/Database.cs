using Dapper;
using Microsoft.Data.Sqlite;
using System.Configuration;

namespace RunTracker
{
    public static class Database
    {
        public static string? ConnectionString { get; set; }

        public static void Initialize()
        {
            using SqliteConnection connection = new SqliteConnection(ConnectionString);
            var sql = "CREATE TABLE IF NOT EXISTS RunSessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, StartTime TEXT, EndTime TEXT, Miles REAL)";
            connection.Execute(sql);
        }
    }
}
