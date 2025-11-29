using MyULibraryBackend.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAlBooksAsync();
        Task<BookDto> GetBookByIdAsync(long id);
        Task<BookDto> GetBookByTitleAsync(string bookTitle);
        Task<List<BookDto>> GetBooksByFilterAsync(BookFilterDto filter);
        Task CreateBookAsync(CreateBookDto request);
        Task UpdateBookAsync(long id, UpdateBookDto request);
        Task DeleteBookAsync(long id);
    }
}
