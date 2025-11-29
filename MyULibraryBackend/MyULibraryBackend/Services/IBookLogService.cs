using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services
{
    public interface IBookLogService
    {
        Task<List<BookLogDto>> GetAllBookLogsAsync();
        Task<BookLogDto?> GetBookLogAsync(long id);
        Task<bool> GetBookLogReservedAsync(long idBook, long idUser);
        Task<List<BookLogDto>> GetByFilterBookLogAsync(BookLogFilter filter);
        Task AddBookLogAsync( CreateBookLogDto request);
        Task AddBookLogListAsync(List<CreateBookLogDto> request);
        Task UpdateBookLogAsync(long id, UpdateBookLogDto request);
        Task DeleteBookLog(long id);
    }
}
