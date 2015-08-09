using NUnit.Framework;
using Codurance.Business.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codurance.Tests.Mocks;
using Moq;
using Codurance.Business.Model.Entities;

namespace Codurance.Business.Commands.Tests
{
	[TestFixture()]
	public class PostCommandHandlerTests
	{
		private ConsoleMock _consoleMock;
		private UserRepositoryMock _userRepositoryMock;
		private MessageRepositoryMock _messageRepositoryMock;

		private PostCommandHandler _postCommandHandler;

		[SetUp]
		public void Setup()
		{
			_consoleMock = new ConsoleMock();
			_userRepositoryMock = new UserRepositoryMock();
			_messageRepositoryMock = new MessageRepositoryMock();

			_postCommandHandler = new PostCommandHandler(_consoleMock.Object, _userRepositoryMock.Object, _messageRepositoryMock.Object);
		}

		[Test()]
		public void HandleTest()
		{
			Assert.AreEqual(true, _postCommandHandler.Handle("User1 -> This is a post").Result);
			Assert.DoesNotThrow(() => _userRepositoryMock.Verify(x => x.Get(It.IsAny<string>()), Times.AtLeast(1)));
			Assert.DoesNotThrow(() => _userRepositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.AtLeast(1)));
			Assert.DoesNotThrow(() => _messageRepositoryMock.Verify(x => x.Create(It.IsAny<Message>()), Times.AtLeast(1)));
		}

		[Test()]
		public void CanHandleTest()
		{
			string username, message;
			Assert.IsTrue(_postCommandHandler.CanHandle("User1 -> This is a post", out username, out message));
			Assert.AreEqual("User1", username);
			Assert.AreEqual("This is a post", message);

			Assert.IsFalse(_postCommandHandler.CanHandle("User1 follows User2", out username, out message));
			Assert.IsFalse(_postCommandHandler.CanHandle("User1", out username, out message));
			Assert.IsFalse(_postCommandHandler.CanHandle("User1 wall", out username, out message));
		}
	}
}