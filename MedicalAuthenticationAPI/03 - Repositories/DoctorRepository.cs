using System;
using Dapper;
using MedicalAuthenticationAPI.Entities;
using MedicalAuthenticationAPI.Infrastructure.Interfaces;
using System.Text;
using MedicalAuthenticationAPI.Repositories.Interfaces;

namespace MedicalAuthenticationAPI.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IUnitOfWork _uow;

        public DoctorRepository(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Doctor> CreateAsync(Doctor user)
        {
            StringBuilder query = new();

            query.Append($" INSERT INTO [dbo].[DOCTOR]");
            query.Append($"    (                    ");
            query.Append($"        UserId,          ");
            query.Append($"        CRM,             ");
            query.Append($"        CreatedAt,       ");
            query.Append($"    )                    ");
            query.Append($"    VALUES               ");
            query.Append($"    (                    ");
            query.Append($"        @UserId,         ");
            query.Append($"        @CRM,            ");
            query.Append($"        @CreatedAt,      ");
            query.Append($"    )                    ");

            DynamicParameters parameters = new();

            parameters.Add("@UserId", user.UserId);
            parameters.Add("@CRM", user.CRM);
            parameters.Add("@CreatedAt", user.CreatedAt);

            using var conn = _uow.Connection;

            await conn.ExecuteAsync(query.ToString(), parameters);

            return user;
        }
    }
}

