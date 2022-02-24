using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        readonly MyULibraryDbContext db;
        public UserRepository(MyULibraryDbContext context)
        {
            db = context;
        }
        public List<User> getAll()
        {
            return db.Users.Include(r => r.Role).ToList();
        }
        public User Get(long id)
        {
            return db.Users.FirstOrDefault(u => u.IdUser == id);
        }
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void Update(User user, User entity)
        {
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.Email = entity.Email;
            user.IdRole = entity.IdRole;
            db.SaveChanges();

        }
        public void Delete(User user)
        {
            db.Remove(user);
            db.SaveChanges();
        }
    }
}
