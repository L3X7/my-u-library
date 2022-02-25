using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class GenreRepository : IGenreRepository
    {

        readonly MyULibraryDbContext db;
        public GenreRepository(MyULibraryDbContext context)
        {
            db = context;
        }

        public void Add(Genre user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Genre user)
        {
            throw new NotImplementedException();
        }

        public Genre Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<Genre> getAll()
        {
            return db.Genres.ToList();
        }

        public void Update(Genre user, Genre entity)
        {
            throw new NotImplementedException();
        }
    }
}
