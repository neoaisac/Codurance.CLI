using NUnit.Framework;
using Codurance.Business.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codurance.Tests.Mocks;

namespace Codurance.Business.Commands.Tests
{
	[TestFixture()]
	public class MainCommandHandlerChainTests
	{
		private ConsoleMock _consoleMock;
		private UserRepositoryMock _userRepositoryMock;

		private MainCommandHandlerChain _mainCommandHandlerChain;

		[SetUp]
		public void Setup()
		{
			_consoleMock = new ConsoleMock();
			_userRepositoryMock = new UserRepositoryMock();

			_mainCommandHandlerChain = new MainCommandHandlerChain(_consoleMock.Object, new[] { new CommandHandlerMock(true).Object });
		}

		[Test()]
		public void HandleTest()
		{
			Assert.IsTrue(_mainCommandHandlerChain.Handle("Any command is handled").Result);

			_mainCommandHandlerChain = new MainCommandHandlerChain(_consoleMock.Object, new[] { new CommandHandlerMock(false).Object });
			Assert.IsFalse(_mainCommandHandlerChain.Handle("Any command is not handled").Result);

			_mainCommandHandlerChain = new MainCommandHandlerChain(_consoleMock.Object, new[] { new CommandHandlerMock(true, "Error").Object });
			Assert.IsTrue(_mainCommandHandlerChain.Handle("Any command is handled").Result);
			Assert.AreEqual("Error", _consoleMock.WriteLineBuffer[0]);
		}
	}
}