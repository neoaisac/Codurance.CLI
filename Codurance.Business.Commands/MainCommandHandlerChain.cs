using System;
using System.Linq;
using System.Threading.Tasks;
using Codurance.Business.Model.Contracts;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Commands
{
	public class MainCommandHandlerChain : ICommandHandlerChain
	{
		private readonly ICommandHandler[] _commandHandlers;
		private readonly IConsole _console;

		public MainCommandHandlerChain(
			IConsole console,
			ICommandHandler[] commandHandlers)
		{
			_console = console;
			_commandHandlers = commandHandlers;
		}

		public async Task<bool> Handle(string element)
		{
			foreach (var commandHandler in _commandHandlers)
			{
				if (await commandHandler.Handle(element))
				{
					HandleErrors(commandHandler);
					return true;
				}
			}

			return false;
		}

		private void HandleErrors(ICommandHandler commandHandler)
		{
			if (commandHandler.Errors != null && commandHandler.Errors.Any())
			{
				_console.ForegroundColor = ConsoleColor.Red;

				foreach (var errorString in commandHandler.Errors)
				{
					_console.WriteLine(errorString);
				}

				commandHandler.Errors.Clear();
				_console.ResetColor();
			}
		}
	}
}