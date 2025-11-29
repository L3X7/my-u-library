using Microsoft.EntityFrameworkCore;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class BookRepository : IBookRepository
    {

        private readonly MyULibraryDbContext _db;
        public BookRepository(MyULibraryDbContext context)
        {
            _db = context;
        }

        public async Task AddAsync(Book book)
        {
            await _db.Books.AddAsync(book);
        }

        public void Delete(Book book)
        {
            _db.Remove(book);
        }

        public async Task<Book?> GetByIdAsync(long id)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByTitleAsync(string title)
        {
            return await _db.Books.FirstOrDefaultAsync(b => b.Title == title);
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<List<Book>> GetByFilterAsync(BookFilterDto filter)
        {
            var query = _db.Books.Include(g => g.Genre).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(b => b.Title.Contains(filter.Title));

            if (!string.IsNullOrWhiteSpace(filter.Author))
                query = query.Where(b => b.Author.Contains(filter.Author));

            if (!string.IsNullOrWhiteSpace(filter.Genre))
                query = query.Where(b => b.Genre.GenreName.Contains(filter.Genre));

            if (filter.Year.HasValue)
                query = query.Where(b => b.PublishedYear == filter.Year);

            return await query.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
