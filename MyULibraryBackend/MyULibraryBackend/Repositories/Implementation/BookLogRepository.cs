using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class BookLogRepository : IBookLogRepository
    {

        private readonly MyULibraryDbContext _db;
        private readonly IMapper _mp;


        public BookLogRepository(MyULibraryDbContext context, IMapper mapper)
        {
            _db = context;
            _mp = mapper;
        }

        public async Task<List<BookLog>> GetAllAsync()
        {
            return await _db.BookLogs.ToListAsync();
        }

        public async Task<BookLog?> GetByIdAsync(long id)
        {
            return await _db.BookLogs.SingleOrDefaultAsync(l => l.Id == id);
        }

        public async Task<bool> GetBookReservedAsync(long idBook, long idUser)
        {
            return await _db.BookLogs.AnyAsync(b => b.BookId == idBook && b.UserId == idUser && b.ReturnedDate == null);
        }

        public async Task<List<BookLog>> GetFilterAsync(BookLogFilter filter)
        {
            var query = _db.BookLogs.Include(u => u.User).Include(b => b.Book).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.BookTitle))
                query = query.Where(b => b.Book.Title.Contains(filter.BookTitle));

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
                query = query.Where(b => b.User.FirstName.Contains(filter.FirstName));

            if (!string.IsNullOrWhiteSpace(filter.LastName))
                query = query.Where(b => b.User.LastName.Contains(filter.LastName));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                query = query.Where(b => b.User.Email == filter.Email);

            if (filter.LoanedDate.HasValue)
                query = query.Where(b => b.LoanedDate == filter.LoanedDate);

            if (filter.ReturnedDate.HasValue)
                query = query.Where(b => b.ReturnedDate == filter.ReturnedDate);

            return await query.ToListAsync();
        }

        public async Task AddAsync(BookLog bookLog)
        {
            await _db.BookLogs.AddAsync(bookLog);
        }

        public async Task AddListAsync(List<BookLog> booksLog)
        {
            await _db.BookLogs.AddRangeAsync(booksLog);
        }

        public void Delete(BookLog bookLog)
        {
            _db.BookLogs.Remove(bookLog);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
