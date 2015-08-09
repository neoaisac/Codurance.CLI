using System;
using Codurance.Shared.Contracts;

namespace Codurance.Shared.Default
{
	public sealed class DefaultConsole : IConsole
	{
		public ConsoleColor ForegroundColor
		{
			get
			{
				return Console.ForegroundColor;
			}

			set
			{
				Console.ForegroundColor = value;
			}
		}

		public void ResetColor()
		{
			Console.ResetColor();
		}

		public string ReadLine()
		{
			return Console.ReadLine();
		}

		public void Write(string line)
		{
			Console.Write(line);
		}

		public void WriteLine(string line)
		{
			Console.WriteLine(line);
		}

		public void WriteLine(string line, params object[] args)
		{
			Console.WriteLine(line, args);
		}
	}
}