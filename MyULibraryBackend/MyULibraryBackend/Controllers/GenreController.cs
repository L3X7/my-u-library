using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Controllers
{
    [Route("api/genre")]
    [ApiController]
    [Authorize(Policy = "RequireAdmin, RequireLibrarian")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                GenreDto genre = await _genreService.GetGenreByIdAsync(id);
                if (genre == null)
                {
                    return NotFound(new { code = 404, message = "Genre not found" });
                }
                return Ok(new { code = 200, message = "Get genre", data = genre });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<GenreDto> genres = await _genreService.GetAllGenresAsync();
                return Ok(new { code = 200, message = "Get genres", data = genres });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGenreDto request)
        {

            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty genre" });
                }
                await _genreService.CreateGenreAsync(request);
                return Ok(new { code = 200, message = "Genre created" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] UpdateGenreDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { code = 400, message = "Empty genre" });
                }
                await _genreService.UpdateGenreAsync(id, request);

                return Ok(new { code = 200, message = "Genre updated" });
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
                await _genreService.DeleteGenreAsync(id);
                return Ok(new { code = 200, message = "Genre deleted" });
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
