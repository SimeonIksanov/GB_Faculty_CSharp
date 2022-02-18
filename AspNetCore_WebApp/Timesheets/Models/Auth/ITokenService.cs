using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Models.Auth
{
    public interface ITokenService
    {
        TokenValidationParameters GetTokenValidationParameters();

        string GenerateAccessToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken(IEnumerable<Claim> claims);
    }
}
