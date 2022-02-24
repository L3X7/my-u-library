using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MyULibraryBackend.Repositories;
using MyULibraryBackend.Entities.Models;

namespace MyULibraryBackend.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository repository)
        {
            roleRepository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Role> roles = roleRepository.getAll();
            return Ok(new { code = 200, message = "Get roles", data = roles });
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Role role = roleRepository.Get(id);
            if (role == null)
            {
                return NotFound(new { code = 404, message = "Role not found" });
            }
            return Ok(new { code = 200, message = "Get user", data = role });
        }

        [HttpPost]
        public IActionResult Post([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest(new { code = 400, message = "Empty role" });
            }
            roleRepository.Add(role);
            return Ok(new { code = 200, message = "Role created" });
        }


        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { code = 400, message = "Empty role" });
            }
            Role roleUpdate = roleRepository.Get(id);
            if (roleUpdate == null)
            {
                return NotFound(new { code = 404, message = "Role not found" });
            }

            return Ok(new { code = 200, message = "Role updated" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            Role role = roleRepository.Get(id);
            if (role == null)
            {
                return NotFound(new { code = 404, message = "Role not found" });
            }
            roleRepository.Delete(role);
            return Ok(new { code = 200, message = "Role deleted" });
        }
    }
}
