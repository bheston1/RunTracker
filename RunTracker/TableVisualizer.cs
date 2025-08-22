using Spectre.Console;

namespace RunTracker
{
    public static class TableVisualizer
    {
        public static void DrawTable(List<RunSession> runSessions)
        {
            var dateTimeFormat = "M/d/yyyy h:m tt";
            var index = 1;

            var table = new Table();
            table.ShowRowSeparators();

            table.AddColumn("#");
            table.AddColumn("Session Id");
            table.AddColumn("Start time");
            table.AddColumn("End time");
            table.AddColumn("Distance (mi)");

            foreach (var session in runSessions)
            {
                table.AddRow(index.ToString(), session.Id.ToString(), session.StartTime.ToString(dateTimeFormat), session.EndTime.ToString(dateTimeFormat), $"{session.Miles} miles");
                index++;
            }

            AnsiConsole.Write(table);
        }
    }
}
