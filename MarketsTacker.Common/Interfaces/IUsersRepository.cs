using MarketsTracker.Model;
using System.Threading.Tasks;

namespace MarketsTracker.Common
{
    public interface IUsersRepository
    {
        Task<User> GetUserById(int userId, int instrumentsPage, int amount);
        Task<bool> IsUserExists(int userId);
        Task<bool> IsUserExists(string userName);
        Task<User> Authenticate(string username, string password);
        Task Create(User user, string password);
        Task Update(User user);
    }
}