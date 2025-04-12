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
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService, IDoctorRepository doctorRepository)
        {
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
            _authService = authService;
        }

        public async Task<string> LoginAsync(UserDto userDto)
        {
            try
            {
                if (userDto is null)
                    throw new Exception("Requisicao inválida");

                var user = await _userRepository.GetUserByEmailAsync(userDto.UserName) ??
                    throw new Exception("Usuário inválido ou inexistente.");

                var hashedPassword = HashPassword.VerifyPassword(userDto.Password, user.Salt, user.HashedPassword);

                if (!hashedPassword)
                    throw new Exception("Usuário inválido ou inexistente");

                var token = _authService.GenerateJwtToken(user);
                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserCreateDto> CreateAsync(UserCreateDto userDto)
        {
            if (userDto is null)
                throw new Exception("Requisicao inválida.");

            var user = await _userRepository.GetUserByEmailAsync(userDto.Email);

            if (user != null)
                throw new Exception("O Usuário informado já existe.");

            var hashedPassword = HashPassword.GenerateHash(userDto.Password, out string salt);

            var userEntity = ParseToEntity(userDto, hashedPassword, salt);

            await _userRepository.CreateAsync(userEntity);
            userDto.Password = null;

            if (userDto.Role.Contains("Doctor"))
            {
                var userRegistered = await _userRepository.GetUserByEmailAsync(userDto.Email);
                if (userRegistered != null)
                    await _doctorRepository.CreateAsync(new Doctor
                    {
                        UserId = userRegistered.Id,
                        CRM = userDto.CRM,
                        CreatedAt = DateTime.Now
                    });
            }

            return userDto;
        }

        private User ParseToEntity(UserCreateDto userDto, string hashedPassword, string salt)
        {
            return new User(userDto.UserName, userDto.Email, hashedPassword, salt, userDto.Role);
        }
    }
}

