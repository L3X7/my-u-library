using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using MyULibraryBackend.Dtos;
using AutoMapper;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class BookLogRepository : IBookLogRepository
    {

        readonly MyULibraryDbContext db;
        private readonly IMapper mp;


        public BookLogRepository(MyULibraryDbContext context, IMapper mapper)
        {
            db = context;
            mp = mapper;
        }

        public void Add(BookLog bookLog)
        {
            throw new NotImplementedException();
        }

        public void AddList(List<BookLog> booksLog)
        {

            using (var transaction = db.Database.BeginTransaction())
            {
                transaction.CreateSavepoint("saveBooksLogAndUpdateBook");
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
                    transaction.RollbackToSavepoint("saveBooksLogAndUpdateBook");
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
            return db.BookLogs.FirstOrDefault(u => u.IdBookLog == id);
        }

        public List<BookLog> getAll()
        {
            throw new NotImplementedException();
        }

        public List<BookLog> Filter(string firstName, string lastName, string email)
        {

            return db.BookLogs.Include(b => b.Book).Include(u => u.User)
                .Where(b => ((firstName == "" || firstName == null) || b.User.FirstName.ToLower().Contains(firstName.Trim().ToLower()))
                && ((lastName == "" || lastName == null) || b.User.LastName.Contains(lastName.Trim().ToLower()))
                && ((email == "" || email == null) || b.User.Email.ToLower().Contains(email.Trim().ToLower())) && b.ReturnedDate == null).ToList();

        }

        public void PatchBookLogAndBook(JsonPatchDocument<BookLogDto> patchBookDto, BookLog entity)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                transaction.CreateSavepoint("updatebookLogAndBook");
                try
                {
                    //.1 Patch element
                    //Patch DTO
                    BookLogDto bookLogToPatch = mp.Map<BookLogDto>(entity);
                    patchBookDto.ApplyTo(bookLogToPatch);

                    //2. Mapper entity with DTO
                    mp.Map(bookLogToPatch, entity);

                    ////3. Update BookLog
                    db.SaveChanges();

                    //2. Update book
                    Book book = db.Books.FirstOrDefault(b => b.IdBook == entity.IdBook);
                    book.Quantity++;
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.RollbackToSavepoint("updatebookLogAndBook");
                    throw;
                }
            }
        }

        public void Update(BookLog bookLog, BookLog entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBookReserved(long idBook, long idUser)
        {
            BookLog bookReserved = db.BookLogs.Where(b => b.IdBook == idBook && b.IdUser == idUser && b.ReturnedDate == null).FirstOrDefault();
            if (bookReserved == null)
            {
                return false;
            }
            return true;
        }
    }
}
