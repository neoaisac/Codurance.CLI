using System.Collections.Generic;
using System.Threading.Tasks;
using Codurance.Business.Model.Contracts;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Commands.Base
{
	public abstract class CommandHandlerBase : ICommandHandler
	{
		public virtual IList<string> Errors { get; protected set; }

		protected readonly IConsole Console;

		protected CommandHandlerBase(
			IConsole console)
		{
			Console = console;
			Errors = new List<string>();
		}

		public abstract Task<bool> Handle(string element);
	}
}