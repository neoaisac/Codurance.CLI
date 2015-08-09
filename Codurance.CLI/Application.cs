using System;
using System.Threading.Tasks;
using Codurance.Business.Model.Contracts;
using Codurance.Shared.Contracts;

namespace Codurance.CLI
{
	internal class Application
	{
		private readonly ICommandHandlerChain _commandHandlerChain;
		private readonly IConsole _console;

		public Application(
			IConsole console,
			ICommandHandlerChain commandHandlerChain)
		{
			_console = console;
			_commandHandlerChain = commandHandlerChain;
		}

		public async Task<int> Run(params string[] args)
		{
			string command = null;
			while (command != "quit")
			{
				bool commandHandled = false;
				try
				{
					if (!string.IsNullOrWhiteSpace(command))
						commandHandled = await _commandHandlerChain.Handle(command);
				}
				catch (Exception ex)
				{
					_console.ForegroundColor = ConsoleColor.Red;
					_console.WriteLine("An error occurred while trying to handle your command:");
					_console.WriteLine(ex.Message);
					_console.ResetColor();
				}

				_console.Write("> ");
				command = _console.ReadLine();
			}

			return 0;
		}
	}
}