namespace RunTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string applicationPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string applicationFolder = Path.Combine(applicationPath, "RunTracker");
            Directory.CreateDirectory(applicationFolder);
            string dbPath = Path.Combine(applicationFolder, "RunSessions.db");
            Database.ConnectionString = $"Data Source={dbPath}";
            
            var repository = new RunRepository(Database.ConnectionString);
            var ui = new UserInterface(repository);
            ui.MainMenu();
        }
    }
}
