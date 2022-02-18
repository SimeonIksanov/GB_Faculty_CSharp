using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Auth;

namespace Domain
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public TokenService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public TokenValidationParameters GetTokenValidationParameters()
            => new TokenValidationParameters()
            {
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
                RoleClaimType = ClaimsIdentity.DefaultRoleClaimType,
                ClockSkew = TimeSpan.Zero
            };

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            return GenerateToken(claims, _jwtOptions.AccessLifetime);

        }

        public string GenerateRefreshToken(IEnumerable<Claim> claims)
        {
            return GenerateToken(claims, _jwtOptions.RefreshLifeTime);
        }

        private string GenerateToken(IEnumerable<Claim> claims, int lifeTime)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(lifeTime)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var securityHandler = new JwtSecurityTokenHandler();
            string token = securityHandler.WriteToken(jwt);
            return token;
        }

        private SecurityKey GetSymmetricSecurityKey()
            => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.SigningKey));
    }
}
