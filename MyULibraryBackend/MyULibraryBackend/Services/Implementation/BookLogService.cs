using AutoMapper;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services.Implementation
{
    public class BookLogService : IBookLogService
    {
        private readonly IBookLogRepository _bookLogRepository;
        private readonly IMapper _mp;
        public BookLogService(IBookLogRepository bookLogRepository, IMapper mapper)
        {
            _bookLogRepository = bookLogRepository;
            _mp = mapper;
        }

        public async Task AddBookLogAsync(CreateBookLogDto request)
        {
            var bookLog = new BookLog
            {
                UserId = request.UserId,
                BookId = request.BookId,
                LoanedDate = request.LoanedDate
            };
            await _bookLogRepository.AddAsync(bookLog);

            await _bookLogRepository.SaveChangesAsync();
        }

        public async Task AddBookLogListAsync(List<CreateBookLogDto> request)
        {
            var booksLog = _mp.Map<List<BookLog>>(request);
            await _bookLogRepository.AddListAsync(booksLog);

            await _bookLogRepository.SaveChangesAsync();
        }

        public async Task DeleteBookLog(long id)
        {
            var bookLog = await _bookLogRepository.GetByIdAsync(id);
            if (bookLog == null)
                throw new KeyNotFoundException($"BookLog with ID {id} not found.");
            _bookLogRepository.Delete(bookLog);

            await _bookLogRepository.SaveChangesAsync();
        }

        public async Task<List<BookLogDto>> GetByFilterBookLogAsync(BookLogFilter filter)
        {
            var bookLogs = await _bookLogRepository.GetFilterAsync(filter);
            return _mp.Map<List<BookLogDto>>(bookLogs);
        }

        public async Task<List<BookLogDto>> GetAllBookLogsAsync()
        {
            var booksLog = await _bookLogRepository.GetAllAsync();
            return _mp.Map<List<BookLogDto>>(booksLog);
        }

        public async Task<BookLogDto?> GetBookLogAsync(long id)
        {
            var bookLog = await _bookLogRepository.GetByIdAsync(id);
            if (bookLog == null)
                throw new KeyNotFoundException($"BookLog with ID {id} not found.");
            return _mp.Map<BookLogDto>(bookLog);
        }

        public async Task<bool> GetBookLogReservedAsync(long idBook, long idUser)
        {
            return await _bookLogRepository.GetBookReservedAsync(idBook, idUser);
        }

        public async Task UpdateBookLogAsync(long id, UpdateBookLogDto request)
        {
            var bookLog = await _bookLogRepository.GetByIdAsync(id);
            if (bookLog == null)
                throw new KeyNotFoundException($"BookLog with ID {id} not found.");
            bookLog.BookId = request.BookId;
            bookLog.UserId = request.UserId;
            bookLog.LoanedDate = request.LoanedDate;
            bookLog.ReturnedDate = request.ReturnedDate;

            await _bookLogRepository.SaveChangesAsync();
        }
    }
}
