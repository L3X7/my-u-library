using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyULibraryBackend.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyULibraryBackend.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _settings;
        public JwtService(IOptions<JwtSettings> options) {
            _settings = options.Value; 
        }

        public (string Token, string Jti) GenerateAccessToken(string username, IEnumerable<string> roles)
        {
            byte[] key = Encoding.UTF8.GetBytes(_settings.Key);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jti = Guid.NewGuid().ToString();

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(ClaimTypes.Name, username)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return (token, jti);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<ClaimsPrincipal?> ValidateToken(string token)
        {
            JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_settings.Key);
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };
            var validationResult = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

            if (validationResult.IsValid)
            {
                return new ClaimsPrincipal(validationResult.ClaimsIdentity);
            }
            return null;
        }

        public async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            byte[] key = Encoding.UTF8.GetBytes(_settings.Key);
            JsonWebTokenHandler tokenHandler = new JsonWebTokenHandler();
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };

            // 1. Validate
            var validationResult = await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);

            // 2. Check if validation failed (malformed token, bad signature, etc.)
            if (!validationResult.IsValid)
            {
                throw new SecurityTokenException("Invalid token");
            }

            // 3. Security check
            if (validationResult.SecurityToken is JsonWebToken jwt)
            {
                //Check the algorithm
                if (!jwt.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token algorithm");
                }
            }
            else
            {
                throw new SecurityTokenException("Invalid token type");
            }

            return new ClaimsPrincipal(validationResult.ClaimsIdentity);
        }
    }
}
