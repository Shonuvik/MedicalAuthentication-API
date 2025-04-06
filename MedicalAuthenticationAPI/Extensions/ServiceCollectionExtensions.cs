using FluentValidation;
using MedicalAuthenticationAPI.Controllers.V1;
using MedicalAuthenticationAPI.Controllers.Validation;
using MedicalAuthenticationAPI.Infrastructure;
using MedicalAuthenticationAPI.Infrastructure.Interfaces;
using MedicalAuthenticationAPI.Repositories;
using MedicalAuthenticationAPI.Repositories.Interfaces;
using MedicalAuthenticationAPI.Services;
using MedicalAuthenticationAPI.Services.Interfaces;

namespace MedicalAuthenticationAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddIoC(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IValidator<UserCreateDto>, RegisterUserValidator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}

