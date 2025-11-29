using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Policy = "RequireAdmin, RequireLibrarian")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                UserDto user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { code = 404, message = "User not found" });
                }
                return Ok(new { code = 200, message = "Get user", data = user });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty user" });
                }
                await _userService.CreateUserAsync(request);
                return Ok(new { code = 200, message = "User created" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateUserDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty user" });
                }
                await _userService.UpdateUserAsync(id, request);

                return Ok(new { code = 200, message = "User updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {

            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok(new { code = 200, message = "User deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UserDto> users = await _userService.GetAllUsersAsync();
                return Ok(new { code = 200, message = "Get users", data = users });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
