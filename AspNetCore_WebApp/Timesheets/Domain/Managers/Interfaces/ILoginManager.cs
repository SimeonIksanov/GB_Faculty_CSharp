using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Domain.Managers.Interfaces;
using Models.Auth;
using Models.Entities;

namespace Domain.Managers.Interfaces
{
    public interface ILoginManager
    {
        /// <summary>
        /// Generates JWT tokens
        /// </summary>
        /// <param name="userRepository">Repository</param>
        /// <param name="user">Instance of User class, must exist in repository</param>
        /// <returns>LoginResponse JWT access token and refresh token</returns>
        Task<LoginResponse> Authenticate(IUserRepository userRepository, User user);

        /// <summary>
        /// Refreshes expired access JWT token
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="userRepository"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<LoginResponse> RefreshTokenPair(
            IUserManager userManager,
            IUserRepository userRepository,
            string token,
            CancellationToken cancellationToken);
    }
}
