using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Repositories;

namespace MyULibraryBackend.Controllers
{
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {

        private readonly IUserRepository userRepository;

        public SecurityController(IUserRepository repository)
        {
            userRepository = repository;
        }

        [HttpPost("login")]
        public IActionResult Post([FromBody] UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new { code = 400, message = "Empty user" });
                }
                UserDto userLogin = userRepository.Login(user);
                if (userLogin == null)
                {
                    return Unauthorized(new { code = 401, message = "Authentication failed" });
                }
                return Ok(new { code = 200, message = "Successful user login", data = userLogin });
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
