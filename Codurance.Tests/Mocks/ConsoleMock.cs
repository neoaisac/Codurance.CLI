using System;
using System.Collections.Generic;
using Codurance.Shared.Contracts;
using Moq;

namespace Codurance.Tests.Mocks
{
	public class ConsoleMock : Mock<IConsole>
	{
		public string ReadLineBuffer { get; set; }
		public ConsoleColor ForegroundColor { get; set; }

		public List<string> WriteLineBuffer { get; set; }

		public ConsoleMock()
		{
			WriteLineBuffer = new List<string>();

			Setup(x => x.ForegroundColor).Returns(ForegroundColor);
			Setup(x => x.ReadLine()).Returns(ReadLineBuffer);
			Setup(x => x.ResetColor());
			Setup(x => x.Write(It.IsAny<string>())).Callback((string line) => WriteLineBuffer.Add(line));
			Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string line) => WriteLineBuffer.Add(line));
			Setup(x => x.WriteLine(It.IsAny<string>())).Callback((string line) => WriteLineBuffer.Add(line));
			Setup(x => x.WriteLine(It.IsAny<string>(), It.IsAny<object[]>())).Callback((string line, object[] args) => WriteLineBuffer.Add(string.Format(line, args)));
		}
	}
}