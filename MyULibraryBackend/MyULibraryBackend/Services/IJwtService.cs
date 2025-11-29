using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services
{
    public interface IJwtService
    {
        (string Token, string Jti) GenerateAccessToken(string username, IEnumerable<string> roles);
        string GenerateRefreshToken();
        Task<ClaimsPrincipal?> ValidateToken(string token);
        Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
    }
}
