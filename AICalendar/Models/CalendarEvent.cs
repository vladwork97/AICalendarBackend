namespace AICalendar.Models
{
    public class CalendarEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[] Participants { get; set; } = Array.Empty<string>();
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}