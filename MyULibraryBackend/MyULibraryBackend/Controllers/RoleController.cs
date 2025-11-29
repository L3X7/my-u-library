using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MyULibraryBackend.Services;
using MyULibraryBackend.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyULibraryBackend.Controllers
{
    [Route("api/role")]
    [ApiController]
    [Authorize(Policy = "RequireAdmin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<RoleDto> roles = await _roleService.GetAllRolesAsync();
                return Ok(new { code = 200, message = "Get roles", data = roles });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                RoleDto role = await _roleService.GetRoleByIdAsync(id);
                if (role == null)
                {
                    return NotFound(new { code = 404, message = "Role not found" });
                }
                return Ok(new { code = 200, message = "Get user", data = role });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRoleDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty role" });
                }
                await _roleService.CreateRoleAsync(request);
                return Ok(new { code = 200, message = "Role created" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateRoleDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty role" });
                }
                await _roleService.UpdateRoleAsync(id, request);

                return Ok(new { code = 200, message = "Role updated" });
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
                await _roleService.DeleteRoleAsync(id);
                return Ok(new { code = 200, message = "Role deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
