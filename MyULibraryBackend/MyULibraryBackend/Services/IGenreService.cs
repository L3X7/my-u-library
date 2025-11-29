using MyULibraryBackend.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> GetGenreByIdAsync(long id);
        Task<GenreDto> GetGenreByNameAsync(string genreNane);
        Task CreateGenreAsync(CreateGenreDto request);
        Task UpdateGenreAsync(long id, UpdateGenreDto request);
        Task DeleteGenreAsync(long id);
    }
}
