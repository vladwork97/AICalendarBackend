using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;

namespace AICalendar.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new();

        [HttpGet]
        public IActionResult GetAll() => Ok(Users);

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            Users.Add(user);
            return Ok(user);
        }

        // GET: /api/v1/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] User updated)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.Email = updated.Email;
            user.Name = updated.Name;

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            Users.Remove(user);
            return Ok("Deleted");
        }
    }
}