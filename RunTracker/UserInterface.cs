using Spectre.Console;

namespace RunTracker
{
    public static class UserInterface
    {
        public static void MainMenu()
        {
            string[] choices =
            {
                "Add new record",
                "View all records",
                "Update existing record",
                "Delete a record",
                "Close app"
            };

            while (true)
            {
                Console.Clear();

                var selection = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Select option").AddChoices(choices));

                switch (selection)
                {
                    case "Add new record":
                        AddRecord();
                        break;

                    case "View all records":
                        ViewRecords(update: false, delete: false);
                        break;

                    case "Update existing record":
                        ViewRecords(update: true, delete: false);
                        break;

                    case "Delete a record":
                        ViewRecords(update: false, delete: true);
                        break;

                    case "Close app":
                        return;
                }
            }
        }

        private static void AddRecord()
        {
            var start = UserInput.GetDateTime("Enter session start date/time (format: m/d/yyyy h:mm am/pm): ");
            var end = UserInput.GetDateTime("Enter session end date/time (format: m/d/yyyy h:mm am/pm): ");
            var miles = AnsiConsole.Ask<double>("Distance (in miles): ");
            RunController.AddSession(start, end, miles);
            AnsiConsole.Markup("\n[green]Record added[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }

        private static void ViewRecords(bool update, bool delete)
        {
            var runSessions = RunController.GetSessions();

            if (!runSessions.Any())
            {
                AnsiConsole.Markup("[yellow]No records found[/]");
                AnsiConsole.Markup("\n\nPress [blue]<enter>[/]");
                Console.ReadLine();
                return;
            }
            else
            {
                TableVisualizer.DrawTable(runSessions);
            }

            switch (update, delete)
            {
                case (false, false):
                    AnsiConsole.Markup("\n\nPress [blue]<enter>[/]");
                    Console.ReadLine();
                    break;

                case (true, false):
                    UpdateRecord();
                    break;

                case (false, true):
                    DeleteRecord();
                    break;
            }
        }

        private static void UpdateRecord()
        {
            int input;

            while (true)
            {
                input = AnsiConsole.Ask<int>("Enter Id of record to update: ");

                if (!RunController.SessionExists(input))
                {
                    AnsiConsole.Markup($"\n[yellow]Record #{input} does not exist[/]\n");
                }
                else
                {
                    break;
                }
            }

            var start = UserInput.GetDateTime("Enter new start date/time (format: m/d/yyyy h:mm am/pm): ");
            var end = UserInput.GetDateTime("Enter new end date/time (format: m/d/yyyy h:mm am/pm): ");
            var miles = AnsiConsole.Ask<double>("New distance (in miles): ");
            RunController.UpdateSession(input, start, end, miles);
            AnsiConsole.Markup("\n[green]Record updated[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }

        private static void DeleteRecord()
        {
            int input;

            while (true)
            {
                input = AnsiConsole.Ask<int>("Enter Id of record to delete: ");

                if (!RunController.SessionExists(input))
                {
                    AnsiConsole.Markup($"\n[yellow]Record #{input} does not exist[/]\n");
                }
                else
                {
                    break;
                }
            }

            RunController.DeleteSession(input);
            AnsiConsole.Markup($"\n[red]Record #{input} deleted[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }
    }
}
