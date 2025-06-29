using ModelContextProtocol.Server;
using System.ComponentModel;

namespace AICalendar.Services;

[McpServerToolType]
public static class Scheduler
{
    [McpServerTool, Description("Schedules an event to users with emails on specific date and time with title")]
    public static string Schedule(
        List<string> emails, DateTime from, DateTime to, string title)
    {
        EventService.CreateEvent(new EventDto
        {
            Title = title,
            Start = from,
            End = to,
            Participants = emails
        });

        return $"Scheduled meeting with title {title} from {from} to {to}";
    }

    [McpServerTool, Description("Get list of events on specific time range.")]
    public static List<EventDto> GetEvents(DateTime from, DateTime to)
    {
        var events = EventService.GetAll().Where(x=>x.Start >=from && x.End <= to);
        return events.ToList();
    }

    [McpServerTool, Description("Cancel event by id")]
    public static bool CancelEvent(Guid id)
    {
        return EventService.DeleteById(id);
    }

    [McpServerTool, Description("Cancel event by title")]
    public static void CancelEvent(string title)
    {
        EventService.DeleteEventsByTitle(title);
    }
}