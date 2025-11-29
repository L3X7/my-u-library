
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Controllers
{
    [Route("api/booklog")]
    [ApiController]
    [Authorize(Policy = "RequireAdmin, RequireLibrarian")]
    public class BookLogController : ControllerBase
    {
        private readonly IBookLogService _bookLogService;

        public BookLogController(IBookLogService bookLogService)
        {
            _bookLogService = bookLogService;
        }

        [HttpPost("list")]
        public async Task<IActionResult> PostList([FromBody] List<CreateBookLogDto> request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty book logs" });
                }
                await _bookLogService.AddBookLogListAsync(request);
                return Ok(new { code = 200, message = "book logs created" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] BookLogFilter request)
        {
            try
            {
                var bookLogs = await _bookLogService.GetByFilterBookLogAsync(request);
                return Ok(new { code = 200, message = "Get book logs", data = bookLogs });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getBookReserved/{idBook}/{idUser}")]
        public async Task<IActionResult> GetBookReserved(long idBook, long idUser)
        {
            try
            {
                bool isReserved = await _bookLogService.GetBookLogReservedAsync(idBook, idUser);

                return Ok(new { code = 200, message = "Book status", data = isReserved });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookLogDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty book logs" });
                }
                await _bookLogService.AddBookLogAsync(request);
                return Ok(new { code = 200, message = "book log created" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(long id, [FromBody] UpdateBookLogDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Book log not found" });
                }

                await _bookLogService.UpdateBookLogAsync(id, request);
                return Ok(new { code = 200, message = "Book log updated" });
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
                await _bookLogService.DeleteBookLog(id);
                return Ok(new { code = 200, message = "Book log deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
