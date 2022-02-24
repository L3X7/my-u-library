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
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository bookRepository;

        public BookController(IBookRepository repository)
        {
            bookRepository = repository;
        }


        [HttpGet("filter")]
        public IActionResult Get(string title = "", string author = "", string genre = "")
        {
            List<Book> books = bookRepository.GetByFilter(title, author, genre);
            return Ok(new { code = 200, message = "Get books", data = books });
        }
    }
}
