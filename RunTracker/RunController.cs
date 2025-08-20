using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker
{
    public static class RunController
    {
        public static void AddSession(DateTime date, DateTime startTime, DateTime endTime, double miles)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "INSERT INTO RunSessions (Date, StartTime, EndTime, Miles) VALUES (@Date, @StartTime, @EndTime, @Miles)";
                connection.Execute(sql, new
                {
                    Date = date.ToString("M/d/yyyy"),
                    StartTime = startTime.ToString("h:m tt"),
                    EndTime = endTime.ToString("h:m tt"),
                    Miles = miles
                });
            }
        }

        public static List<RunSession> GetSessions()
        {
            var runSessions = new List<RunSession>();

            // do shit

            return runSessions;
        }
    }
}
