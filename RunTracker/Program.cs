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
            Database.Initialize();
            UserInterface.MainMenu();
        }
    }
}
