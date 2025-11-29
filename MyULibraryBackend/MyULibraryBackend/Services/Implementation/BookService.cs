using AutoMapper;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mp;

        public BookService(IBookRepository bookRepository, IMapper mp)
        {
            _bookRepository = bookRepository;
            _mp = mp;
        }

        public async Task CreateBookAsync(CreateBookDto request)
        {
            var book = _bookRepository.GetByTitleAsync(request.Title);
            if (book != null)
                throw new Exception($"Book '{request.Title}' is already taken.");
            var newBook = new Book
            {
                Author = request.Author,
                Title = request.Title,
                GenreId = request.IdGenre,
                PublishedYear = request.PublishedYear,
                Quantity = request.Quantity,
            };
            await _bookRepository.AddAsync(newBook);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(long id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if(book == null)
                throw new Exception($"Book with ID {id} not found.");
            _bookRepository.Delete(book);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task<List<BookDto>> GetAlBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mp.Map<List<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(long id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mp.Map<BookDto>(book);
        }

        public async Task<BookDto> GetBookByTitleAsync(string bookTitle)
        {
            var book = await _bookRepository.GetByTitleAsync(bookTitle);
            return _mp.Map<BookDto>(book);
        }

        public async Task<List<BookDto>> GetBooksByFilterAsync(BookFilterDto filter)
        {
            var books = await _bookRepository.GetByFilterAsync(filter);
            return _mp.Map<List<BookDto>>(books);
        }

        public async Task UpdateBookAsync(long id, UpdateBookDto request)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new Exception($"Book with ID {id} not found.");
            book.Title = request.Title;
            book.Author = request.Author;
            book.GenreId = request.IdGenre;
            book.PublishedYear = request.PublishedYear;
            book.Quantity = request.Quantity;
            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
        }
    }
}
