using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IBookLogRepository
    {
        Task<List<BookLog>> GetAllAsync();
        Task<BookLog?> GetByIdAsync(long id);
        Task<bool> GetBookReservedAsync(long idBook, long idUser);
        Task<List<BookLog>> GetFilterAsync(BookLogFilter filter);
        Task AddAsync(BookLog bookLog);
        Task AddListAsync(List<BookLog> booksLog);
        void Delete(BookLog bookLog);
        Task SaveChangesAsync();
    }
}