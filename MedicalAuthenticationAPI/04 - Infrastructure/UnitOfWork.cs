using System;
using System.Data;
using MedicalAuthenticationAPI.Helpers;
using MedicalAuthenticationAPI.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace MedicalAuthenticationAPI.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private readonly DatabaseOptions _databaseOption;
        private IDbTransaction _transaction;

        public UnitOfWork(IOptions<DatabaseOptions> databaseOption)
        {
            _databaseOption = databaseOption.Value;
            _connection = new SqlConnection(_databaseOption.connectionString);
        }

        public IDbConnection Connection
        {
            get
            {
                _connection = new SqlConnection(_databaseOption.connectionString);
                _connection.Open();
                return _connection;
            }
        }

        public IDbTransaction Transaction => _transaction;

        public void Begin()
        {
            _transaction = _connection?.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}

