using Dapper;
using Microsoft.Data.Sqlite;

namespace RunTracker
{
    public static class RunController
    {
        public static void AddSession(DateTime startTime, DateTime endTime, double miles)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "INSERT INTO RunSessions (StartTime, EndTime, Miles) VALUES (@StartTime, @EndTime, @Miles)";
                connection.Execute(sql, new { StartTime = startTime.ToString(), EndTime = endTime.ToString(), Miles = miles });
            }
        }

        public static List<RunSession> GetSessions()
        {
            var runSessions = new List<RunSession>();

            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "SELECT * FROM RunSessions";
                runSessions = connection.Query<RunSession>(sql).ToList();
            }

            return runSessions;
        }

        public static void UpdateSession(int id, DateTime newStartTime, DateTime newEndTime, double newMiles)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "UPDATE RunSessions SET StartTime = @StartTime, EndTime = @EndTime, Miles = @Miles WHERE Id = @Id";
                connection.Execute(sql, new { Id = id, StartTime = newStartTime, EndTime = newEndTime, Miles = newMiles });
            }
        }

        public static void DeleteSession(int id)
        {
            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "DELETE FROM RunSessions WHERE Id = @Id";
                connection.Execute(sql, new { Id = id });
            }
        }

        public static bool SessionExists(int id)
        {
            bool exists;

            using (var connection = new SqliteConnection(Database.ConnectionString))
            {
                var sql = "SELECT 1 FROM RunSessions WHERE Id = @Id";
                exists = connection.ExecuteScalar<bool>(sql, new { Id = id });
            }

            return exists;
        }
    }
}
