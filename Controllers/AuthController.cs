using Microsoft.AspNetCore.Mvc;
using Token_based_Authentication___Authorization.Models;
using Token_based_Authentication___Authorization.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Token_based_Authentication___Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Perform authentication logic using the AuthService
            if (IsValidUser(loginModel))
            {
                var token = _authService.GenerateJwtToken(loginModel.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private bool IsValidUser(LoginModel loginModel)
        {
            // Your authentication logic to validate the user
            // You may want to perform checks against your database or other authentication mechanisms
            // Return true if the user is valid, false otherwise
            return true; // Placeholder - replace with actual validation logic
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SecureResourceController : ControllerBase
    {
        private readonly AuthService _authService;

        public SecureResourceController(AuthService authService)
        {
            _authService = authService;
        }

        [Authorize] // This requires the request to be authenticated
        [HttpGet("resource")]
        public IActionResult GetSecureResource()
        {
            // The user is authenticated, you can perform authorized actions here
            return Ok("This is a secure resource.");
        }
    }
}
