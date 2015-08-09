using System.Collections.Generic;
using System.Threading.Tasks;
using Codurance.Business.Model.Contracts;

namespace Codurance.Business.Model.Base
{
	public abstract class CommandHandlerChainBase : ICommandHandlerChain
	{
		protected virtual ICollection<ICommandHandler> CommandHandlerChain { get; set; }

		protected CommandHandlerChainBase(
			ICommandHandler[] commandHandlerChain)
		{
			CommandHandlerChain = commandHandlerChain;
		}

		public virtual async Task<bool> Handle(string command)
		{
			bool ok = false;

			foreach (var commandHandler in CommandHandlerChain)
			{
				ok = await commandHandler.Handle(command);
				if (ok) break;
			}

			return ok;
		}
	}
}