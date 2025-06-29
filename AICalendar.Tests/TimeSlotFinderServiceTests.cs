using AICalendar.Models;
using AICalendar.Services;
using Xunit;

namespace AICalendar.Tests
{
    public class TimeSlotFinderServiceTests
    {
        [Fact]
        public void FindFreeSlot_ReturnsCorrectSlot_WhenAvailable()
        {
            // Arrange
            var events = new List<CalendarEvent>
            {
                new CalendarEvent
                {
                    Participants = new[] { "alice@email.com" },
                    Start = new DateTime(2025, 6, 15, 10, 0, 0),
                    End = new DateTime(2025, 6, 15, 11, 0, 0)
                }
            };

            var participants = new List<User>
            {
                new User { Email = "alice@email.com", Name = "Alice" },
                new User { Email = "bob@email.com", Name = "Bob" }
            };

            var from = new DateTime(2025, 6, 15, 9, 0, 0);
            var to = new DateTime(2025, 6, 15, 18, 0, 0);
            var duration = TimeSpan.FromHours(1);

            // Act
            var result = TimeSlotFinderService.FindFreeSlot(events, participants, from, to, duration);

            // Assert
            Assert.Equal(new DateTime(2025, 6, 15, 9, 0, 0), result);
        }
    }
}