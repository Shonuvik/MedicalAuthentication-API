using MedicalAuthenticationAPI.Controllers.V1;

namespace MedicalAuthenticationAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginAsync(UserDto userDto);

        Task<UserCreateDto> CreateAsync(UserCreateDto userDto);
    }
}

