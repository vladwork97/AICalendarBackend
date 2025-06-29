using AICalendar.Models;

public static class EventService
{
    private static readonly List<EventDto> _events = new();

    public static void DeleteEventsByTitle(string title)
    {
        _events.RemoveAll(e => e.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public static bool DeleteById(Guid id)
    {
        var eventToDelete = _events.FirstOrDefault(e => e.Id == id);
        if (eventToDelete == null)
            return false;

        _events.Remove(eventToDelete);
        return true;
    }

    public static void CreateEvent(EventDto newEvent)
    {
        _events.Add(newEvent);
    }

    public static IEnumerable<EventDto> GetAll() => _events;
}