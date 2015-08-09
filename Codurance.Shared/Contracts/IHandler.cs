using System.Threading.Tasks;

namespace Codurance.Shared.Contracts
{
	public interface IHandler<T>
	{
		Task<bool> Handle(T element);
	}
}