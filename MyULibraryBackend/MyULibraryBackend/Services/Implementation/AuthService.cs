using Azure.Core;
using Microsoft.IdentityModel.JsonWebTokens;
using MyULibraryBackend.Dtos;
using MyULibraryBackend.Entities.Models;
using MyULibraryBackend.Models;
using MyULibraryBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthService(IJwtService jwtService, IPasswordHasherService passwordHasherService, IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _jwtService = jwtService;
            _passwordHasherService = passwordHasherService;
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<AuthResponse> LoginAsync(LoginDto request)
        {
            var user = await _userRepository.GetByUsernameWithRolesAsync(request.Username);
            if (user == null)
            {
                throw new Exception("Username/password incorrect");
            }
            if (!_passwordHasherService.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Username/password incorrect");
            }

            List<string> roles = user.Roles.Select(r => r.RoleName).ToList();
            var (accessToken, jti) = _jwtService.GenerateAccessToken(user.Username, roles);
            string refreshTokenString = _jwtService.GenerateRefreshToken();

            RefreshToken refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshTokenString,
                CreatedDate = DateTime.UtcNow,
                ExpiredDate = DateTime.UtcNow.AddDays(7),
                JwtId = jti,
            };

            user.RefreshTokens.Add(refreshTokenEntity);
            await _userRepository.SaveChangesAsync();
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenString
            };
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenDto request)
        {
            // 1. Extract the Claims from the expired Access Token
            var principal = await _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);

            // 2. Get the Jti (ID) from the token
            var jti = principal.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;

            var storedRefreshToken = await _refreshTokenRepository.GetByRefreshTokenWithUserRolesAsync(request.RefreshToken);

            //Validations
            if (storedRefreshToken == null)
                throw new Exception("Token does not exist");
            if (DateTime.UtcNow > storedRefreshToken.ExpiredDate)
                throw new Exception("Token expired");
            if (storedRefreshToken.RevokedDate != null)
                throw new Exception("Token revoked");
            if (storedRefreshToken.JwtId != jti)
                throw new Exception("Invalid Token Pairing");

            storedRefreshToken.RevokedDate = DateTime.UtcNow;

            List<string> roles = storedRefreshToken.User.Roles.Select(r => r.RoleName).ToList();
            var (newAccessToken, newJti) = _jwtService.GenerateAccessToken(storedRefreshToken.User.Username, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            var newRefreshTokenEntity = new RefreshToken
            {
                UserId = storedRefreshToken.User.Id,
                Token = newRefreshToken,
                CreatedDate = DateTime.UtcNow,
                ExpiredDate = DateTime.UtcNow.AddDays(7),
                JwtId = newJti,
            };

            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();
            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task RegisterAsync(RegisterDto request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user != null)
            {
                throw new Exception("Username already exist");
            }
            var roles = await _roleRepository.GetByIdsAsync(request.Roles);
            if (!roles.Any())
            {
                throw new Exception("User has not roles configurated");
            }
            string passwordHash = _passwordHasherService.Hash(request.Password);
            User newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                PasswordHash = passwordHash,
                CreatedDate = DateTime.UtcNow,
                Email = request.Email,
                Roles = new List<Role>()
            };

            newUser.Roles.AddRange(roles);
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetByRefreshTokenAsync(refreshToken);
            if (storedRefreshToken == null)
                throw new Exception("Token not found");
            if (storedRefreshToken.RevokedDate == null)
            {
                storedRefreshToken.RevokedDate = DateTime.UtcNow;
                await _refreshTokenRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The token has already been revoked.");
            }

        }
    }
}
