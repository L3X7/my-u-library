using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Services;
using MyULibraryBackend.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Authorize(Policy = "RequireAdmin, RequireLibrarian")]
        public async Task<IActionResult> Post([FromBody] CreateBookDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty book" });
                }
                await _bookService.CreateBookAsync(request);
                return Ok(new { code = 200, message = "Book created" });
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdmin, RequireLibrarian")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateBookDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty book" });
                }
                await _bookService.UpdateBookAsync(id, request);

                return Ok(new { code = 200, message = "Book updated" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireAdmin, RequireLibrarian")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Ok(new { code = 200, message = "Book deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("search")]
        [Authorize(Policy = "RequireAdmin, RequireLibrarian, RequireStudent")]
        public async Task<IActionResult> Search([FromQuery] BookFilterDto filter)
        {
            try
            {
                List<BookDto> books = await _bookService.GetBooksByFilterAsync(filter);
                return Ok(new { code = 200, message = "Get books", data = books });
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "RequireAdmin, RequireLibrarian, RequireStudent")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                BookDto book = await _bookService.GetBookByIdAsync(id);
                return Ok(new { code = 200, message = "Get book", data = book });
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdmin, RequireLibrarian")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<BookDto> books = await _bookService.GetAlBooksAsync();
                return Ok(new { code = 200, message = "Get books", data = books });
            }
            catch (System.Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
