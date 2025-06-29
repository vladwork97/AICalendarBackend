using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;

namespace AICalendar.Controllers
{
    [ApiController]
    [Route("api/v1/events/{eventId}/participants")]
    public class ParticipantsController : ControllerBase
    {
        private static readonly Dictionary<Guid, List<User>> EventParticipants = new();

        // GET: /api/v1/events/{eventId}/participants
        [HttpGet]
        public IActionResult Get(Guid eventId)
        {
            EventParticipants.TryGetValue(eventId, out var participants);
            return Ok(participants ?? new List<User>());
        }

        // POST: /api/v1/events/{eventId}/participants
        [HttpPost]
        public IActionResult Add(Guid eventId, [FromBody] User user)
        {
            if (!EventParticipants.ContainsKey(eventId))
                EventParticipants[eventId] = new List<User>();

            EventParticipants[eventId].Add(user);
            return Ok(user);
        }

        // PUT: /api/v1/events/{eventId}/participants/{userId}
        [HttpPut("{userId}")]
        public IActionResult Update(Guid eventId, Guid userId, [FromBody] User updated)
        {
            if (!EventParticipants.TryGetValue(eventId, out var list))
                return NotFound("Event not found");

            var user = list.FirstOrDefault(u => u.Id == userId);
            if (user == null) return NotFound("User not found");

            user.Name = updated.Name;
            user.Email = updated.Email;
            return Ok(user);
        }

        // DELETE: /api/v1/events/{eventId}/participants/{userId}
        [HttpDelete("{userId}")]
        public IActionResult Delete(Guid eventId, Guid userId)
        {
            if (!EventParticipants.TryGetValue(eventId, out var list))
                return NotFound("Event not found");

            var user = list.FirstOrDefault(u => u.Id == userId);
            if (user == null) return NotFound("User not found");

            list.Remove(user);
            return Ok("Participant removed");
        }
    }
}