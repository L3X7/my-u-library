using Microsoft.EntityFrameworkCore;
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

        private readonly MyULibraryDbContext _db;
        public RoleRepository(MyULibraryDbContext context)
        {
            _db = context;
        }

        public async Task AddAsync(Role role)
        {
            await _db.Roles.AddAsync(role);
        }

        public void Delete(Role role)
        {
            _db.Roles.Remove(role);
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _db.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(long id)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Role>> GetByIdsAsync(List<long> roles)
        {
            return await _db.Roles.Where(a => roles.Contains(a.Id)).ToListAsync();
        }

        public async Task<Role?> GetByRoleNameAsync(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
