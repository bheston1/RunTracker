using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker
{
    public static class Menu
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

                        break;

                    case "View all records":

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
    }
}
