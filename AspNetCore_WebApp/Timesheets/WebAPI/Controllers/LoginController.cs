using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Domain.Extensions;
using Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Models.Auth;
using WebAPI.Models.Dto;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        const string COOKIE_NAME = "refreshToken";
        private readonly ILogger<LoginController> _logger;
        private readonly IUserManager _userManager;
        private readonly ILoginManager _loginManager;

        public LoginController(
            ILogger<LoginController> logger,
            IUserManager userManager,
            ILoginManager loginManager)
        {
            _logger = logger;
            _userManager = userManager;
            _loginManager = loginManager;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login(
            [FromQuery] LoginRequestDto request,
            [FromServices] IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            //todo автомапер.. я про тебя помню
            LoginRequest loginRequest = new LoginRequest
            {
                Login = request.Login,
                Password = request.Password
            };
            var user = await _userManager.GetUser(userRepository, loginRequest, cancellationToken);
            if (user.IsEmptyObject())
            {
                return Unauthorized();
            }
            var responseTokens = await _loginManager.Authenticate(userRepository, user);

            SetTokenCookie(responseTokens.RefreshToken);
            return Ok(responseTokens.AccessToken);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh(
            [FromServices] IUserRepository userRepository,
            [FromServices] IUserManager userManager,
            CancellationToken cancellationToken)
        {
            string oldRefreshToken = Request.Cookies[COOKIE_NAME];
            var responseTokens = await _loginManager.RefreshTokenPair(userManager, userRepository, oldRefreshToken, cancellationToken);
            SetTokenCookie(responseTokens.RefreshToken);
            return Ok(responseTokens.AccessToken);
        }

        [HttpPost("createUser")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserRequestDto createUserRequestDto,
            [FromServices] IUserRepository userRepository,
            CancellationToken cancellationToken)
        {
            var createUserRequest = new CreateUserRequest
            {
                Username = createUserRequestDto.Username,
                Password = createUserRequestDto.Password
            };
            await _userManager.CreateUser(userRepository, createUserRequest, cancellationToken);
            return Ok();
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),

            };
            Response.Cookies.Append(COOKIE_NAME, token, cookieOptions);
        }
    }
}
