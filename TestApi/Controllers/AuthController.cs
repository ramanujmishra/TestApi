
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;

namespace TestApi.Controllers
{
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
    }
}
