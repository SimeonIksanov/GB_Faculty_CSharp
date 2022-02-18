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
        Task<LoginResponse> Authenticate(IUserRepository userRepository, User user);

        Task<LoginResponse> RefreshTokenPair(
            IUserManager userManager,
            IUserRepository userRepository,
            string token,
            CancellationToken cancellationToken);
    }
}
