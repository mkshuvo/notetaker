using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteTakerServer.Services;
using NoteTakerServer.Models;

namespace NoteTakerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private JwtTokenService _jwtTokenService;
        public AuthController(AuthService authService, JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _authService = authService;
        }
        [HttpPost("signup")]
        public IActionResult Signup([FromBody] User user)
        {
            if (_authService.IsUserExists(user.Email))
            {
                return BadRequest(new UserError() { ErrorCode = "400", Message = "User already exists" });
            }
            // send user with token
            var response = _authService.Signup(user);
            return Ok(new { Message = "User created successfully", response });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var user = _authService.ValidateUser(loginDTO.Email, loginDTO.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var updatedToken = _authService.UpdateAccessToken(user);
            return Ok(new { Token = updatedToken, user });
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _authService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            if (!_authService.IsUserExists(user.Email))
            {
                return NotFound("User not found.");
            }

            _authService.UpdateUser(user);
            return Ok(new { Message = "User updated successfully.", user });
        }
        [HttpDelete("{email}")]
        public IActionResult DeleteUser(string email)
        {
            if (!_authService.IsUserExists(email))
            {
                return NotFound("User not found.");
            }

            _authService.DeleteUser(email);
            return Ok(new { Message = "User deleted successfully." });
        }
    }
}
