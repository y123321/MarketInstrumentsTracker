using MarketsTracker.Common;
using MarketsTracker.DAL;
using MarketsTracker.DAL.Entities;
using MarketsTracker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MarketsTracker.DAL
{

    public class UsersRepository : IUsersRepository
    {

        private readonly string _connectionString;
        public UsersRepository(DatabaseOptions options)
        {
            _connectionString = options.ConnectionString;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            UserEntity user = await GetUserEntityByUserName(username);
            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            var res = Mappers.MapToUserDto(user);

            // authentication successful
            return res;
        }

        private async Task<UserEntity> GetUserEntityByUserName(string userName)
        {
            var sql = @"SELECT u.userId, u.userName ,u.firstName, u.lastName,u.passwordHash,u.passwordSalt
                        FROM[user] u 
                        WHERE u.userName=@UserName";
            var prams = new SqlParameter("@UserName", userName);
            var queryResult = await QueryExecuter.ExecuteCollection(sql, e => Mappers.MapToUserEntity(e, true), _connectionString, prams);
            var user = queryResult.FirstOrDefault();
            return user;
        }
        public async Task<bool> IsUserExists(string userName)
        {
            var sql = @"SELECT TOP 1 1
                        FROM [user] u
                        WHERE u.userName=@UserName";
            var prams = new SqlParameter("@UserName", userName);
            var res = await QueryExecuter.ExecuteScalar(sql, _connectionString, prams);
            var exists = res != null;
            return exists;
        }
        public async Task<bool> IsUserExists(int userId)
        {
            var sql = @"SELECT TOP 1 1
                        FROM [user] u
                        WHERE u.userId=@UserId";
            var prams = new SqlParameter("@UserId", userId);
            var res = await QueryExecuter.ExecuteScalar(sql, _connectionString, prams);
            var exists = res != null;
            return exists;
        }
        private async Task<string> GetUserName(int userId)
        {
            var sql = @"SELECT TOP 1 UserName
                        FROM [user] u
                        WHERE u.userId=@UserId";
            var prams = new SqlParameter("@UserId", userId);
            var res = await QueryExecuter.ExecuteScalar(sql, _connectionString, prams);
            return res.ToString();
        }
        public async Task Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");


            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);


            var sql = @"
                            INSERT INTO [user] (userName , passwordHash, passwordSalt, firstName, lastName) 
                            VALUES(@UserName,@Hash,@Salt,@FirstName,@LastName);
                        ";
                       
            var hashParam = new SqlParameter("@Hash", SqlDbType.Binary);
            hashParam.Value = passwordHash;
            var saltParam = new SqlParameter("@Salt", SqlDbType.Binary);
            saltParam.Value = passwordSalt;
            var prams = new SqlParameter[]
            {
                new SqlParameter("@UserName",user.UserName),
                saltParam,
                hashParam,
                new SqlParameter("@FirstName",user.FirstName),
                new SqlParameter("@LastName", user.LastName)
            };
            await QueryExecuter.ExecuteNoQuery(sql, _connectionString, prams);
        }

        public async Task<User> GetUserById(int userId)
        {
            var sql = @"SELECT u.userId, u.userName ,u.firstName, u.lastName,i.instrumentId,i.name,i.symbol,i.instrumentType
                        FROM[user] u
                        LEFT JOIN userInstrument ui
                        ON u.userId = ui.userId
                        LEFT JOIN Instrument i
                        ON i.instrumentId = ui.instrumentId
                        WHERE u.userId=@UserId";
            var prams = new SqlParameter("@UserId", userId);
            User user = await GetUser(sql, prams);
            return user;
        }

        private async Task<User> GetUser(string sql, SqlParameter prams)
        {
            Func<SqlCommand, Task<ICollection<User>>> func = async cmd =>
            {
                var reader = await cmd.ExecuteReaderAsync();
                User user = null;
                while (await reader.ReadAsync())
                {
                    if (user == null)
                        user = Mappers.MapToUserEntity(reader, false)
                            .MapToUserDto();
                    if (user.UserId != (int)reader["userId"])
                        throw new DataException("results returned for more that one user");
                    var instrument = Mappers.MapToInstrumentEntity(reader)
                        .MapToInstrumentDto();
                    if(instrument.InstrumentId>0)
                        user.Instruments.Add(instrument);
                }
                return new User[] { user };
            };
            var queryResult = await QueryExecuter.ExecuteSql(sql, _connectionString, func, prams);
            var user = queryResult.FirstOrDefault();
            return user;
        }

        /// <summary>
        /// Update a user without updating the insruments list
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Update(User user)
        {
            if (user.UserId <= 0)
                throw new ArgumentException($"user id: {user.UserId} is invalid. User might not exist");
            var userName = await GetUserName(user.UserId);
            if (userName == null)
                throw new ArgumentException("No user exists with the user id of " + user.UserId);

            var sql = $@"
                          UPDATE [user]
                          SET userName=@UserName,
                              firstName=@FirstName,
                              lastName=@LastName,
                          WHERE userId=@UserId";
            var prams = new SqlParameter[] 
            {
                new SqlParameter("@UserName",user.UserName),
                new SqlParameter("@FirstName",user.FirstName),
                new SqlParameter("@LastName",user.LastName),
                new SqlParameter("@UserId",user.UserId),
                new SqlParameter("@IsUserNameChanged",userName != user.UserName),
            };
            await QueryExecuter.ExecuteNoQuery(sql, _connectionString, prams);
        }
        // private helper methods

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
