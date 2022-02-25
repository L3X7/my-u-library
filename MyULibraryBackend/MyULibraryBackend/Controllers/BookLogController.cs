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
    [Route("api/booklog")]
    [ApiController]
    public class BookLogController : ControllerBase
    {
        private readonly IBookLogRepository bookRepository;

        public BookLogController(IBookLogRepository repository)
        {
            bookRepository = repository;
        }

        [HttpPost("addBooksLog")]
        public IActionResult Post([FromBody] List<BookLog> booksLog)
        {
            if (booksLog == null)
            {
                return BadRequest(new { code = 400, message = "Empty books log" });
            }
            bookRepository.AddList(booksLog);
            return Ok(new { code = 200, message = "books log created" });
        }
    }
}
