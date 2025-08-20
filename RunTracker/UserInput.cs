using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTracker
{
    public static class UserInput
    {
        public static DateTime GetDateTime(string prompt)
        {
            DateTime dateTime;
            var format = "M/d/yyyy h:m tt";

            while (true)
            {
                var input = AnsiConsole.Ask<string>(prompt);

                if (!DateTime.TryParseExact(input, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime))
                {
                    AnsiConsole.Markup("\n[yellow]Invalid format[/]\n");
                }
                else
                {
                    break;
                }
            }

            return dateTime;
        }
    }
}
