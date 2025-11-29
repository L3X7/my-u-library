using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(long id);
        Task<Genre?> GetByNameAsync(string genreName);
        Task AddAsync(Genre user);
        void Delete(Genre user);
        Task SaveChangesAsync();
    }
}
