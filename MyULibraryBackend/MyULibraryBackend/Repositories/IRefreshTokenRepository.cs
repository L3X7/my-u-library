using MyULibraryBackend.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyULibraryBackend.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        void Delete(RefreshToken refreshToken);
        Task<RefreshToken?> GetByIdAsync(long id);
        Task<List<RefreshToken>> getAllAsync();
        Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken);
        Task<RefreshToken?> GetByRefreshTokenWithUserRolesAsync(string refreshToken);
        Task SaveChangesAsync();
    }
}
