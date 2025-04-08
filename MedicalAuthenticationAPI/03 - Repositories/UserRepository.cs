using System.Text;
using Dapper;
using MedicalAuthenticationAPI.Entities;
using MedicalAuthenticationAPI.Infrastructure.Interfaces;
using MedicalAuthenticationAPI.Repositories.Interfaces;

namespace MedicalAuthenticationAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _uow;

        public UserRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            StringBuilder query = new();

            query.Append($" SELECT                                              ");
            query.Append($"    Id        AS {nameof(User.Id)},                  ");
            query.Append($"    UserName  AS {nameof(User.UserName)},            ");
            query.Append($"    Email     AS {nameof(User.Email)},               ");
            query.Append($"    Hash      AS {nameof(User.HashedPassword)},      ");
            query.Append($"    Salt      AS {nameof(User.Salt)},                ");
            query.Append($"    CreatedAt AS {nameof(User.CreatedAt)},           ");
            query.Append($"    Role      AS {nameof(User.Role)}                 ");
            query.Append($" FROM [USER]                                         ");
            query.Append($" WHERE Email = @Email                                ");

            DynamicParameters parameters = new();
            parameters.Add("@Email", email);

            using var conn = _uow.Connection;

            var result = await conn.QueryFirstOrDefaultAsync<User>(query.ToString(), parameters);

            return result;
        }

        public async Task<User> CreateAsync(User user)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO [dbo].[USER]");
            query.Append($"    (                    ");
            query.Append($"        UserName,        ");
            query.Append($"        Email,           ");
            query.Append($"        Hash,            ");
            query.Append($"        Salt,            ");
            query.Append($"        CreatedAt,       ");
            query.Append($"        Role             ");
            query.Append($"    )                    ");
            query.Append($"    VALUES               ");
            query.Append($"    (                    ");
            query.Append($"        @UserName,       ");
            query.Append($"        @Email,          ");
            query.Append($"        @Hash,           ");
            query.Append($"        @Salt,           ");
            query.Append($"        @CreatedAt,      ");
            query.Append($"        @Role            ");
            query.Append($"    )                    ");

            DynamicParameters parameters = new();

            parameters.Add("@UserName", user.UserName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Hash", user.HashedPassword);
            parameters.Add("@Salt", user.Salt);
            parameters.Add("@CreatedAt", user.CreatedAt);
            parameters.Add("@Role", user.Role);

            using var conn = _uow.Connection;

            await conn.ExecuteAsync(query.ToString(), parameters);

            return user;
        }
    }
}

