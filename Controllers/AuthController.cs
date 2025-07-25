using Microsoft.AspNetCore.Mvc;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;

namespace CareerConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Register(dto);
            if (result == "Email already exists.")
                return Conflict(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Login(dto);
            if (result == "Login successful.")
                return Ok(result);
            else
                return Unauthorized(result);
        }
    }
}
