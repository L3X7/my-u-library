using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IBookRepository
    {
        List<Book> getAll();
        Book Get(long id);
        List<Book> GetByFilter(string title, string author, string genre);
        void Add(Book user);
        void Update(Book user, Book entity);
        void Delete(Book user);
    }
}
