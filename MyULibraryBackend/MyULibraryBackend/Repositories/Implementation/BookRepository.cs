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

        public void Add(Book user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book user)
        {
            throw new NotImplementedException();
        }

        public Book Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<Book> getAll()
        {
            throw new NotImplementedException();
        }

        public List<Book> GetByFilter(string title = "", string author = "", string genre = "")
        {

            return db.Books.Include(b => b.Genre).Where(b => (title == "" || b.Title.Contains(title)) && (author == "" || b.Author.Contains(author)) && (genre == "" || b.Genre.GenreName.Contains(author))).ToList();

        }

        public void Update(Book user, Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
