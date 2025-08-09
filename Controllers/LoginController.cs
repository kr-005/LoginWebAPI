using Microsoft.AspNetCore.Mvc;
using LoginWebAPI.Model;//For using the Model Class

namespace LoginWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            // Simple hardcoded validation
            if (req.Username == "Karthik" && req.Password == "pass123")
            {
                // Normally you'd return a token
                return Ok(new { WelcomeMsg = "Welcome to Angular Project ", user = "Youe API Working "+req.Username });
            }
            return Unauthorized(new { message = "Invalid credentials" });
        }


    }
}
