public class EventDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public List<string> Participants { get; set; } = new();
}
