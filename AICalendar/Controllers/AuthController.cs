using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;


[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        //  Validate the login request
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Email and password are required.");
        }

        //  If valid, return a dummy token
        return Ok(new { token = "dummy-token", user = request.Email });
    }
}
