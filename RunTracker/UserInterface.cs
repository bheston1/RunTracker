using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var date = UserInput.GetDateTime("Enter session date (m/d/yyyy): ", "M/d/yyyy");
            var start = UserInput.GetDateTime("Enter start time (h:mm am/pm): ", "h:m tt");
            var end = UserInput.GetDateTime("Enter end time (h:mm am/pm): ", "h:m tt");
            var miles = AnsiConsole.Ask<double>("Distance (in miles): ");
            RunController.AddSession(date, start, end, miles);
            AnsiConsole.Markup("\n[green]Record added[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }

        private static void ViewRecords(bool update, bool delete)
        {
            var runSessions = RunController.GetSessions();

            if (!runSessions.Any())
            {
                AnsiConsole.Markup("[yellow]No records found[/]");
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
