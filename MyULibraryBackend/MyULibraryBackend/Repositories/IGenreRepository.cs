using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IGenreRepository
    {
        List<Genre> getAll();
        Genre Get(long id);
        void Add(Genre user);
        void Update(Genre user, Genre entity);
        void Delete(Genre user);
    }
}
