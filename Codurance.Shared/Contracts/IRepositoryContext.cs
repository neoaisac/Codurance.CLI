using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codurance.Shared.Contracts
{
	public interface IRepositoryContext
	{
		IEnumerable<T> GetCollection<T>() where T : class;

		Task<T> Create<T>(T element) where T : class;

		Task<T> Update<T>(T element) where T : class;

		Task<bool> Delete<T>(T element) where T : class;

		Task<int> SaveChanges();
	}
}