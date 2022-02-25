using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IBookLogRepository
    {
        List<BookLog> getAll();
        BookLog Get(long id);
        void Add(BookLog bookLog);
        void AddList(List<BookLog> booksLog);
        void Update(BookLog bookLog, BookLog entity);
        void Delete(BookLog bookLog);
    }
}
