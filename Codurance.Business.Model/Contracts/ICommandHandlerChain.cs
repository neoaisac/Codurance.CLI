using Codurance.Shared.Contracts;

namespace Codurance.Business.Model.Contracts
{
	public interface ICommandHandlerChain : IHandlerChain<string>
	{
	}
}