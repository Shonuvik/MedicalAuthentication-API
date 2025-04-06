using MedicalAuthenticationAPI.Entities;

namespace MedicalAuthenticationAPI.Repositories.Interfaces
{
    public interface IUserRepository
	{
        Task<User> GetUserByEmailAsync(string email);

        Task<User> CreateAsync(User user);
    }
}

