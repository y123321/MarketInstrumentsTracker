using MarketsTracker.Common;
using MarketsTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketsTracker.Services
{
    public class UsersService :IUsersService
    {
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Authenticate(string userName, string password) =>await  _repository.Authenticate(userName, password);

        public async Task<User> Get(int id) => await _repository.GetUserById(id);

        public async Task<bool> IsUserExists(int userId) => await _repository.IsUserExists(userId);
        public async Task<bool> IsUserExists(string userName) => await _repository.IsUserExists(userName);

        public async Task Register(RegistrationRequest user) => await _repository.Create(user,user.Password);

        public async Task Update(User user) => await _repository.Update(user);
    }
}
