using MedicalAuthenticationAPI.Entities;

namespace MedicalAuthenticationAPI.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor> CreateAsync(Doctor user);
    }
}

