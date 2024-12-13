using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteTakerServer.Services;
using NoteTakerServer.Models;

namespace NoteTakerServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AuthService _authService;
        private JwtTokenService _jwtTokenService;
        public UsersController(AuthService authService, JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _authService = authService;
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
