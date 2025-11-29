using MyULibraryBackend.Dtos;
using MyULibraryBackend.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto request);
        Task<AuthResponse> LoginAsync(LoginDto request);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenDto request);
        Task RevokeTokenAsync(string refreshToken);

    }
}
