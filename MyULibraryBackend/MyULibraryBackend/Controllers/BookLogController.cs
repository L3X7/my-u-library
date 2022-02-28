using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Dtos;
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
        private readonly IBookLogRepository bookLogRepository;
        private readonly IMapper mp;

        public BookLogController(IBookLogRepository repository, IMapper mapper)
        {
            bookLogRepository = repository;
            mp = mapper;
        }

        [HttpPost("addBooksLog")]
        public IActionResult PostList([FromBody] List<BookLogDto> booksLogDto)
        {
            if (booksLogDto == null)
            {
                return BadRequest(new { code = 400, message = "Empty book logs" });
            }
            List<BookLog> booksLog = mp.Map<List<BookLog>>(booksLogDto);
            bookLogRepository.AddList(booksLog);
            return Ok(new { code = 200, message = "book logs created" });
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(long id, [FromBody] JsonPatchDocument<BookLogDto> patchBookDto)
        {
            if(patchBookDto == null)
            {
                return BadRequest(new { code = 400, message = "book log not found" });
            }

            BookLog bookLogEntity = bookLogRepository.Get(id);
            if (bookLogEntity == null)
            {
                return NotFound(new { code = 404, message = "book log not found" });
            }

            bookLogRepository.PatchBookLogAndBook(patchBookDto, bookLogEntity);
            return Ok(new { code = 200, message = "book log updated" });
        }

        [HttpGet("filter")]
        public IActionResult Filter(string firstName = "", string lastName = "", string email = "")
        {
            List<BookLog> booksLog = bookLogRepository.Filter(firstName, lastName, email);
            List<BookLogDto> booksLogDto = mp.Map<List<BookLogDto>>(booksLog);
            return Ok(new { code = 200, message = "Get book logs", data = booksLogDto });
        }

        [HttpGet("getBookReserved/{idBook}/{idUser}")]
        public IActionResult GetBookReserved(int idBook, int idUser)
        {
            bool isReserved = bookLogRepository.GetBookReserved(idBook, idUser);
            if (isReserved)
            {
                return Unauthorized(new { code = 401, message = "Book not available" });
            }
            else
            {
                return Ok(new { code = 200, message = "Book available" });
            }
            
        }
    }
}
