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
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository genreRepository;

        public GenreController(IGenreRepository repository)
        {
            genreRepository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Genre> books = genreRepository.getAll();
                return Ok(new { code = 200, message = "Get genres", data = books });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
