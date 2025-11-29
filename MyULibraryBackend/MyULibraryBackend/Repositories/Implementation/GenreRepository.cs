using Microsoft.EntityFrameworkCore;
using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class GenreRepository : IGenreRepository
    {

        private readonly MyULibraryDbContext _db;
        public GenreRepository(MyULibraryDbContext context)
        {
            _db = context;
        }

        public async Task AddAsync(Genre genre)
        {
            await _db.Genres.AddAsync(genre);
        }

        public void Delete(Genre genre)
        {
            _db.Genres.Remove(genre);
        }

        public async Task<Genre?> GetByIdAsync(long id)
        {
            return await _db.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _db.Genres.ToListAsync();
        }

        public async Task<Genre?> GetByNameAsync(string genreName)
        {
            return await _db.Genres.FirstOrDefaultAsync(g => g.GenreName == genreName);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
