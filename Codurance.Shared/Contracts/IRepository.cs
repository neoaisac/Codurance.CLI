using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codurance.Shared.Contracts
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> Get();

		Task<T> Create(T element);

		Task<T> Update(T element);

		Task<bool> Delete(T element);
	}
}