namespace RunTracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database.Initialize();
            Menu.MainMenu();
        }
    }
}
