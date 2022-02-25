using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class BookLogRepository : IBookLogRepository
    {

        readonly MyULibraryDbContext db;
        public BookLogRepository(MyULibraryDbContext context)
        {
            db = context;
        }

        public void Add(BookLog bookLog)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<BookLog> booksLog)
        {

            using (var transaction = db.Database.BeginTransaction())
            {
                transaction.CreateSavepoint("saveBooksLog");
                try
                {
                    db.BookLogs.AddRange(booksLog);
                    db.SaveChanges();

                    //get ID's books
                    List<long> idsBook = booksLog.Select(bl => bl.IdBook).ToList();
                    List<Book> books = db.Books.Where(b => idsBook.Contains(b.IdBook)).ToList();

                    //Update books
                    foreach (var item in books)
                    {
                        item.Quantity--;
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.RollbackToSavepoint("saveBooksLog");
                    throw;
                }
            }

        }

        public void Delete(BookLog bookLog)
        {
            throw new NotImplementedException();
        }

        public BookLog Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<BookLog> getAll()
        {
            throw new NotImplementedException();
        }

        public void Update(BookLog bookLog, BookLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
