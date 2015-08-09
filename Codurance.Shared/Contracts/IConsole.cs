using System;

namespace Codurance.Shared.Contracts
{
	public interface IConsole
	{
		ConsoleColor ForegroundColor { get; set; }

		string ReadLine();

		void Write(string line);

		void WriteLine(string line);

		void WriteLine(string line, params object[] args);

		void ResetColor();
	}
}