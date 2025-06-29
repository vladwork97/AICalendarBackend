using AICalendar.DTOs;
using System.Text.RegularExpressions;

namespace AICalendar.Services
{
    public static class PromptProcessor
    {
        public static string Process(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                return "Prompt is empty.";

            // CANCEL EVENTS
            var cancelMatch = Regex.Match(prompt, "cancel all events with title '(.*?)'", RegexOptions.IgnoreCase);
            if (cancelMatch.Success)
            {
                var title = cancelMatch.Groups[1].Value;
                EventService.DeleteEventsByTitle(title);
                return $"Deleted events with title '{title}'";
            }

            // ADD MEETING
            var addMatch = Regex.Match(prompt, "add (?:a\\s)?meeting with (\\w+) on (\\w+) from (\\d{1,2}:\\d{2}) to (\\d{1,2}:\\d{2})", RegexOptions.IgnoreCase);
            if (addMatch.Success)
            {
                var person = addMatch.Groups[1].Value;
                var dayOfWeek = addMatch.Groups[2].Value;
                var start = addMatch.Groups[3].Value;
                var end = addMatch.Groups[4].Value;

                var date = GetNextWeekdayDate(dayOfWeek);
                if (!DateTime.TryParse($"{date:yyyy-MM-dd} {start}", out var startTime) ||
                    !DateTime.TryParse($"{date:yyyy-MM-dd} {end}", out var endTime))
                {
                    return "Invalid time format.";
                }

                EventService.CreateEvent(new EventDto
                {
                    Title = $"Meeting with {person}",
                    Start = startTime,
                    End = endTime,
                    Participants = new List<string> { person }
                });

                return $"Scheduled meeting with {person} on {date:dddd, dd MMMM yyyy} from {start} to {end}";
            }

            return $"Could not understand prompt: {prompt}";
        }

        private static DateTime GetNextWeekdayDate(string weekdayName)
        {
            var dayOfWeek = Enum.Parse<DayOfWeek>(weekdayName, ignoreCase: true);
            var today = DateTime.Today;
            int daysUntil = ((int)dayOfWeek - (int)today.DayOfWeek + 7) % 7;
            return today.AddDays(daysUntil == 0 ? 7 : daysUntil);
        }
    }
}