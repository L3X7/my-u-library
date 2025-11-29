using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(long id);
        Task<Book?> GetByTitleAsync(string title);
        Task<List<Book>> GetByFilterAsync(BookFilterDto filter);
        Task AddAsync(Book book);
        void Delete(Book book);
        Task SaveChangesAsync();
    }
}
