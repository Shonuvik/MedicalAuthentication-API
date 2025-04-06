using System.Data;

namespace MedicalAuthenticationAPI.Infrastructure.Interfaces
{
    public interface IUnitOfWork
	{
		public IDbConnection Connection { get;}

		public IDbTransaction Transaction { get;}
	}
}

