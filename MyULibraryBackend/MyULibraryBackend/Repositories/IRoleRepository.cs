using MyULibraryBackend.Entities.Models;
using System.Collections.Generic;

namespace MyULibraryBackend.Repositories
{
    public interface IRoleRepository
    {
        List<Role> getAll();
        Role Get(long id);
        void Add(Role user);
        void Update(Role user, Role entity);
        void Delete(Role user);
    }
}
