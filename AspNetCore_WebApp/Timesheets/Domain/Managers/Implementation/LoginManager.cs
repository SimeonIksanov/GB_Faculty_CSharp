using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Managers.Interfaces;
using Models.Auth;
using Models.Entities;

namespace Domain.Managers.Implementation
{
    public sealed class LoginManager : ILoginManager
    {
        ITokenService _tokenService;

        public LoginManager(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Authenticate(IUserRepository userRepository, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            var loginResponse = new LoginResponse()
            {
                AccessToken = _tokenService.GenerateAccessToken(claims),
                RefreshToken = _tokenService.GenerateRefreshToken(claims)
                //ExpiresIn = accessTokenRaw.ValidTo.ToEpochTime()
            };

            user.RefreshToken = loginResponse.RefreshToken;
            await userRepository.Update(user);

            return loginResponse;
        }

        public async Task<LoginResponse> RefreshTokenPair(
            IUserManager userManager,
            IUserRepository userRepository,
            string token,
            CancellationToken cancellationToken)
        {
            var user = await userManager.FindUserByToken(token, cancellationToken);
            if (user.IsEmptyObject())
            {
                return new LoginResponse();
            }
            return await Authenticate(userRepository, user);
        }
    }
}
