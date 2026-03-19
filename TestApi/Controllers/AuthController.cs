
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Controllers
{
    //[ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Hardcoded user for demo
            if (request.Username == "admin" && request.Password == "1234")
            {
                var token = _tokenService.CreateToken(request.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [HttpPost("loginWithHttpOnlyCookie")]
        public IActionResult LoginForHttpOnlyCookie(LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "1234")
            {
                var token = _tokenService.CreateToken(request.Username);
                Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });
                return Ok(new { message = "Login successful, token set in HttpOnly cookie" });
            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
            return Ok(new { message = "Logged out, token cookie deleted" });
        }
    }
}
