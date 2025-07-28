using Microsoft.AspNetCore.Mvc;
using CareerConnect.DTOs;
using CareerConnect.Interfaces;

namespace CareerConnect.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authRepository.Register(dto);
            if (result == "Email already exists.")
                return Conflict(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authRepository.Login(dto);
            if (result == "Login successful.")
                return Ok(result);
            else
                return Unauthorized(result);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var result = _authRepository.DeleteUser(userId);
            if (result == "User not found.")
                return NotFound(result);

            return Ok(result);
        }
    }
}
