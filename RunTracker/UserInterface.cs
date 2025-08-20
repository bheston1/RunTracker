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
                        ViewRecords();
                        break;

                    case "Update existing record":

                        break;

                    case "Delete a record":

                        break;

                    case "Close app":
                        return;
                }
            }
        }

        private static void AddRecord()
        {
            var date = UserInput.GetDateTime("Enter session date (m/d/yyyy): ", "M/d/yyyy");
            var start = UserInput.GetDateTime("Enter start time (h:m am/pm): ", "h:m tt");
            var end = UserInput.GetDateTime("Enter end time (h:m am/pm): ", "h:m tt");
            var miles = AnsiConsole.Ask<double>("Distance (in miles): ");
            RunController.AddSession(date, start, end, miles);
            AnsiConsole.Markup("\n[green]Record added[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }

        private static void ViewRecords()
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

            AnsiConsole.Markup("\n\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }
    }
}
