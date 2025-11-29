using Microsoft.EntityFrameworkCore;
using MyULibraryBackend.Entities;
using MyULibraryBackend.Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories.Implementation
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        readonly MyULibraryDbContext _db;
        public RefreshTokenRepository(MyULibraryDbContext context)
        {
            _db = context;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _db.RefreshTokens.AddAsync(refreshToken);
        }

        public void Delete(RefreshToken refreshToken)
        {
            _db.RefreshTokens.Remove(refreshToken);
        }

        public async Task<RefreshToken?> GetByIdAsync(long id)
        {
            return await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RefreshToken>> getAllAsync()
        {
            return await _db.RefreshTokens.ToListAsync();
        }

        public async Task<RefreshToken?> GetByRefreshTokenAsync(string token)
        {
            return await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByRefreshTokenWithUserRolesAsync(string refreshToken)
        {
            return await _db.RefreshTokens.Include(u => u.User).ThenInclude(r => r.Roles).FirstOrDefaultAsync(r => r.Token == refreshToken);
        }
    }
}
