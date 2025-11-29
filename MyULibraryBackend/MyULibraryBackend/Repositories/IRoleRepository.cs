using MyULibraryBackend.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync();
        Task<List<Role>> GetByIdsAsync(List<long> ids);
        Task<Role?> GetByIdAsync(long id);
        Task<Role?> GetByRoleNameAsync(string roleName);
        Task AddAsync(Role role);
        void Delete(Role role);
        Task SaveChangesAsync();
    }
}
