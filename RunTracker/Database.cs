using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker
{
    public static class Database
    {
        public static string ConnectionString = ConfigurationManager.AppSettings.Get("SQLite Connection String");

        public static void Initialize()
        {
            using SqliteConnection connection = new SqliteConnection(ConnectionString);
            var sql = "CREATE TABLE IF NOT EXISTS RunSessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, StartTime TEXT, EndTime TEXT, Miles REAL)";
            connection.Execute(sql);
        }
    }
}
