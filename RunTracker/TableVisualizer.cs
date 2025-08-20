using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace RunTracker
{
    public static class TableVisualizer
    {
        public static void DrawTable(List<RunSession> runSessions)
        {
            var dateTimeFormat = "M/d/yyyy h:m tt";

            var table = new Table();

            table.AddColumn("Id");
            table.AddColumn("Date");
            table.AddColumn("Start time");
            table.AddColumn("End time");
            table.AddColumn("Distance (mi)");

            foreach (var session in runSessions)
            {
                table.AddRow(session.Id.ToString(), session.StartTime.ToString(dateTimeFormat), session.EndTime.ToString(dateTimeFormat), $"{session.Miles} miles");
            }

            AnsiConsole.Write(table);
        }
    }
}
