using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {

        readonly MyULibraryDbContext db;
        public RoleRepository(MyULibraryDbContext context)
        {
            db = context;
        }

        public void Add(Role user)
        {
            throw new NotImplementedException();
        }

        public void Delete(Role user)
        {
            throw new NotImplementedException();
        }

        public Role Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<Role> getAll()
        {
            return db.Roles.ToList();
        }

        public void Update(Role user, Role entity)
        {
            throw new NotImplementedException();
        }
    }
}
