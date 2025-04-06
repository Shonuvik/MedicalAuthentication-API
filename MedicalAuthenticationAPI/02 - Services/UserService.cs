using MedicalAuthenticationAPI.Controllers.V1;
using MedicalAuthenticationAPI.Entities;
using MedicalAuthenticationAPI.Helpers;
using MedicalAuthenticationAPI.Repositories.Interfaces;
using MedicalAuthenticationAPI.Services.Interfaces;

namespace MedicalAuthenticationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<string> LoginAsync(UserDto userDto)
        {
            if (userDto is null)
                throw new Exception("Requisicao inválida");

            var user = await _userRepository.GetUserByEmailAsync(userDto.UserName);

            if (user is null)
                throw new Exception("Usuário inválido ou inexistente.");

            var hashedPassword = HashPassword.GenerateHash(userDto.Password, user.Salt);

            if (hashedPassword != user.HashedPassword)
                throw new Exception("Usuário inválido ou inexistente");

            var token = _authService.GenerateJwtToken(user);
            return token;
        }

        public async Task<UserCreateDto> CreateAsync(UserCreateDto userDto)
        {
            if (userDto is null)
                throw new Exception("Requisicao inválida.");

            var user = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (user != null)
                throw new Exception("O Usuário informado já existe.");

            var salt = HashPassword.Salt();
            var hashedPassword = HashPassword.GenerateHash("password", salt);

            var userEntity = ParseToEntity(userDto, hashedPassword, salt);

            await _userRepository.CreateAsync(userEntity);
            userDto.Password = null;

            return userDto;
        }

        private User ParseToEntity(UserCreateDto userDto, string hashedPassword, string salt)
        {
            return new User(userDto.UserName, userDto.Email, userDto.Password, hashedPassword, salt, userDto.Role);
        }
    }
}

