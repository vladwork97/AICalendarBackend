namespace AICalendar.Models
{
    public class SlotRequest
    {
        public List<User> Participants { get; set; } = new();
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public TimeSpan Duration { get; set; }
    }
}