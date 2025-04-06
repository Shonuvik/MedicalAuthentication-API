using System;
using MedicalAuthenticationAPI.Entities;

namespace MedicalAuthenticationAPI.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
    }
}

