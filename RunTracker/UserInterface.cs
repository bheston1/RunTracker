using Spectre.Console;

namespace RunTracker
{
    public class UserInterface
    {
        private readonly RunRepository _repository;

        public UserInterface(RunRepository repository)
        {
            _repository = repository;
        }

        public void MainMenu()
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

        private void AddRecord()
        {
            DateTime start, end;

            while (true)
            {
                start = InputValidation.GetDateTime("Enter session start date/time (format: m/d/yyyy h:mm am/pm): ");
                end = InputValidation.GetDateTime("Enter session end date/time (format: m/d/yyyy h:mm am/pm): ");

                if (end <= start)
                {
                    AnsiConsole.Markup("\n[yellow]Start time must be before end time[/]\n");
                }
                else
                {
                    break;
                }
            }

            var miles = InputValidation.GetMiles("Enter distance (in miles): ");
            _repository.AddSession(start, end, miles);
            AnsiConsole.Markup("\n[green]Record added[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }

        private void ViewRecords(bool update, bool delete)
        {
            var runSessions = _repository.GetSessions();

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

        private void UpdateRecord()
        {
            int input;

            while (true)
            {
                input = AnsiConsole.Ask<int>("Enter Id of record to update: ");

                if (!_repository.SessionExists(input))
                {
                    AnsiConsole.Markup($"\n[yellow]Record #{input} does not exist[/]\n");
                }
                else
                {
                    break;
                }
            }

            DateTime start, end;

            while (true)
            {
                start = InputValidation.GetDateTime("Enter new start date/time (format: m/d/yyyy h:mm am/pm): ");
                end = InputValidation.GetDateTime("Enter new end date/time (format: m/d/yyyy h:mm am/pm): ");

                if (end <= start)
                {
                    AnsiConsole.Markup("\n[yellow]Start time must be before end time[/]\n");
                }
                else
                {
                    break;
                }
            }
            
            var miles = InputValidation.GetMiles("New distance (in miles): ");
            _repository.UpdateSession(input, start, end, miles);
            AnsiConsole.Markup("\n[green]Record updated[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }

        private void DeleteRecord()
        {
            int input;

            while (true)
            {
                input = AnsiConsole.Ask<int>("Enter Id of record to delete: ");

                if (!_repository.SessionExists(input))
                {
                    AnsiConsole.Markup($"\n[yellow]Record #{input} does not exist[/]\n");
                }
                else
                {
                    break;
                }
            }

            _repository.DeleteSession(input);
            AnsiConsole.Markup($"\n[red]Record #{input} deleted[/]\nPress [blue]<enter>[/]");
            Console.ReadLine();
        }
    }
}
