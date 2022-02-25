using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class BookRepository : IBookRepository
    {

        readonly MyULibraryDbContext db;
        public BookRepository(MyULibraryDbContext context)
        {
            db = context;
        }

        public void Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void Delete(Book book)
        {
            throw new NotImplementedException();
        }

        public Book Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<Book> getAll()
        {
            return db.Books.Include(g => g.Genre).ToList();
        }

        public List<Book> GetByFilter(string title = "", string author = "", string genre = "")
        {
            if((title == "" || title == null) && (author == "" || author == null) && (genre == "" || genre == null))
            {
                return db.Books.Include(b => b.Genre).ToList();
            }
            else
            {
                return db.Books.Include(b => b.Genre).Where(b => ((title == "" || title == null) || b.Title.ToLower().Contains(title.Trim().ToLower())) && ((author == "" || author == null) || b.Author.ToLower().Contains(author.Trim().ToLower())) && ((genre == "" || genre == null) || b.Genre.GenreName.ToLower().Contains(genre.Trim().ToLower()))).ToList();
            }            

        }

        public void Update(Book book, Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
