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

            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "SELECT * FROM RunSessions";
                var reader = connection.ExecuteReader(sql);
                while (reader.Read())
                {
                    runSessions.Add(new RunSession
                    {
                        Date = DateTime.Parse(reader.GetString(0)),
                        StartTime = DateTime.Parse(reader.GetString(1)),
                        EndTime = DateTime.Parse(reader.GetString(2)),
                        Miles = reader.GetDouble(3)
                    });
                }
            }

                return runSessions;
        }

        public static void UpdateSession(DateTime newDate, DateTime newStartTime, DateTime newEndTime, double newMiles)
        {

        }

        public static void DeleteSession(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "DELETE FROM RunSessions WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }
    }
}
