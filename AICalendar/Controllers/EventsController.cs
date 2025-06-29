using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;
using AICalendar.Services;
using System.Text.RegularExpressions;
using AICalendar.DTOs;
using System.Linq;


namespace AICalendar.Controllers
{
    [ApiController]
    [Route("api/v1/events")]
    public class EventsController : ControllerBase
    {

        // GET: /api/v1/events
        [HttpGet]
        public IActionResult GetAll() => Ok(EventService.GetAll());

        // POST: /api/v1/events
        [HttpPost]
        public IActionResult Create([FromBody] EventDto ev)
        {
            EventService.CreateEvent(ev);
            return Ok(ev);
        }

        // GET: /api/v1/events/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var ev = EventService.GetAll().FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        // PUT: /api/v1/events/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] EventDto updated)
        {
            var ev = EventService.GetAll().FirstOrDefault(e => e.Id == id);
            if (ev == null) return NotFound();

            ev.Title = updated.Title;
            ev.Description = updated.Description;
            ev.Participants = updated.Participants.ToList();
            ev.Start = updated.Start;
            ev.End = updated.End;

            return Ok(ev);
        }

        // DELETE: /api/v1/events/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var deleted = EventService.DeleteById(id);
            if (!deleted)
                return NotFound();

            return Ok("Deleted");
        }

        [HttpPost("find-slot")]
        public IActionResult FindFreeSlot([FromBody] SlotRequest request)
        {
            var events = EventService.GetAll()
                .Select(e => new CalendarEvent
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description ?? string.Empty,
                    Start = e.Start,
                    End = e.End,
                    Participants = e.Participants.ToArray()
                })
                .ToList();

            var result = TimeSlotFinderService.FindFreeSlot(
                events,
                request.Participants,
                request.From,
                request.To,
                request.Duration);

            if (result == null)
                return NotFound("No available slot");

            return Ok(new { start = result });
        }

        // GET: /api/v1/events/next-week
        [HttpGet("next-week")]
        public IActionResult GetEventsForNextWeek()
        {
            var today = DateTime.Today;
            var nextWeek = today.AddDays(7);
            var nextWeekEvents = EventService.GetAll().Where(e => e.Start >= today && e.Start < nextWeek).ToList();
            return Ok(nextWeekEvents);
        }

        // GET: /api/v1/events/next-week/table
        [HttpGet("next-week/table")]
        public ContentResult GetEventsForNextWeekAsTable()
        {
            var today = DateTime.Today;
            var nextWeek = today.AddDays(7);
            var nextWeekEvents = EventService.GetAll().Where(e => e.Start >= today && e.Start < nextWeek).ToList();

            var html = "<table border='1'><tr><th>Title</th><th>Description</th><th>Participants</th><th>Start</th><th>End</th></tr>";
            foreach (var ev in nextWeekEvents)
            {
                html += $"<tr>"
                    + $"<td>{System.Net.WebUtility.HtmlEncode(ev.Title)}</td>"
                    + $"<td>{System.Net.WebUtility.HtmlEncode(ev.Description)}</td>"
                    + $"<td>{string.Join(", ", ev.Participants.Select(System.Net.WebUtility.HtmlEncode))}</td>"
                    + $"<td>{ev.Start}</td>"
                    + $"<td>{ev.End}</td>"
                    + "</tr>";
            }
            html += "</table>";
            return Content(html, "text/html");
        }

        [HttpPost("prompt")]
        public IActionResult HandlePrompt([FromBody] PromptRequest request)
        {
            var prompt = request.Prompt ?? string.Empty;
            var response = PromptProcessor.Process(prompt);
            return Ok(response);
        }

    }
}