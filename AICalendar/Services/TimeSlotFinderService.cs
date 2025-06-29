using AICalendar.Models;

namespace AICalendar.Services
{
    public class TimeSlotFinderService
    {
        public static DateTime? FindFreeSlot(
            List<CalendarEvent> allEvents,
            List<User> participants,
            DateTime from,
            DateTime to,
            TimeSpan requiredDuration)
        {
            // Filter events for the specified participants 
            var relevantEvents = allEvents
                .Where(e => e.Participants.Any(p => participants.Any(u => u.Email == p)))
                .OrderBy(e => e.Start)
                .ToList();

            // Initialize the cursor to the start time
            var cursor = from;

            foreach (var ev in relevantEvents)
            {
                if (cursor + requiredDuration <= ev.Start)
                    return cursor;

                if (ev.End > cursor)
                    cursor = ev.End;
            }

            // Check after the last event
            if (cursor + requiredDuration <= to)
                return cursor;

            return null; //  No free slot found
        }
    }
}