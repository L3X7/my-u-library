﻿using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Repositories;
using System.Collections.Generic;

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


        [HttpGet]
        public IActionResult Get()
        {
            List<Book> books = bookRepository.getAll();
            return Ok(new { code = 200, message = "Get books", data = books });
        }


        [HttpGet("filter")]
        public IActionResult Get(string title = "", string author = "", string genre = "")
        {
            List<Book> books = bookRepository.GetByFilter(title, author, genre);
            return Ok(new { code = 200, message = "Get books", data = books });
        }


        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest(new { code = 400, message = "Empty book" });
            }
            Book bookAlreadyExits = bookRepository.GetByTitle(book.Title);
            if (bookAlreadyExits != null)
            {
                return Conflict(new { code = 409, message = "Book already exist" });
            }
            bookRepository.Add(book);
            return Ok(new { code = 200, message = "Book created" });
        }

    }
}
