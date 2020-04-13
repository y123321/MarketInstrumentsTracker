using MarketsTracker.Model;
using System.Threading.Tasks;

namespace MarketsTracker.Common
{
    public interface IUsersService
    {
        Task<User> Get(int id);
        Task Register(RegistrationRequest user);
        Task Update(User user);
        Task<bool> IsUserExists(int userId);
        Task<bool> IsUserExists(string userName);
        Task<User> Authenticate(string userName, string password);
    }
}