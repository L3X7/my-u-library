using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository userRepository;

        public UserController(IUserRepository repository)
        {
            userRepository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<User> users = userRepository.getAll();
                return Ok(new { code = 200, message = "Get users", data = users });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                User user = userRepository.Get(id);
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
        public IActionResult Post([FromBody] User user)
        {

            try
            {
                if (user == null)
                {
                    return BadRequest(new { code = 400, message = "Empty user" });
                }
                User userAlreadyExist = userRepository.GetByEmail(user.Email);
                if (userAlreadyExist != null)
                {
                    return Conflict(new { code = 409, message = "User already exist" });
                }
                userRepository.Add(user);
                return Ok(new { code = 200, message = "User created" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }


        }


        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new { code = 400, message = "Empty user" });
                }
                User userUpdate = userRepository.Get(id);
                if (userUpdate == null)
                {
                    return NotFound(new { code = 404, message = "User not found" });
                }

                return Ok(new { code = 200, message = "User updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {

            try
            {
                User user = userRepository.Get(id);
                if (user == null)
                {
                    return NotFound(new { code = 404, message = "User not found" });
                }
                userRepository.Delete(user);
                return Ok(new { code = 200, message = "User deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
