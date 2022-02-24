using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IUserRepository
    {
        List<User> getAll();
        User Get(long id);
        void Add(User user);
        void Update(User user, User entity);
        void Delete(User user);
    }
}
