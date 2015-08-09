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
	public class WallCommandHandlerTests
	{
		private ConsoleMock _consoleMock;
		private UserRepositoryMock _userRepositoryMock;
		private MessageRepositoryMock _messageRepositoryMock;

		private WallCommandHandler _wallCommandHandler;

		[SetUp]
		public void Setup()
		{
			_consoleMock = new ConsoleMock();
			_userRepositoryMock = new UserRepositoryMock();
			_messageRepositoryMock = new MessageRepositoryMock();

			_wallCommandHandler = new WallCommandHandler(_consoleMock.Object, _userRepositoryMock.Object, _messageRepositoryMock.Object);
		}

		[Test()]
		public void HandleTest()
		{
			var user1 = new User() { Id = Guid.NewGuid(), Username = "User1" };
			var user2 = new User() { Id = Guid.NewGuid(), Username = "User2" };
			var message1 = new Message() { Id = Guid.NewGuid(), UserId = user1.Id, User = user1, Content = "Test1", TimeStamp = DateTime.UtcNow.AddMinutes(-2) };
			var message2 = new Message() { Id = Guid.NewGuid(), UserId = user2.Id, User = user2, Content = "Test2", TimeStamp = DateTime.UtcNow.AddMinutes(-1) };

			user1.Following = new List<User>() { user2 };
			user1.Messages = new List<Message>() { message1 };
			user2.Messages = new List<Message>() { message2 };

			_userRepositoryMock.CachedUsers = new List<User>() { user1, user2 };
			_messageRepositoryMock.CachedMessages = new List<Message>() { message1, message2 };

			Assert.AreEqual(true, _wallCommandHandler.Handle("User1 wall").Result);
			Assert.DoesNotThrow(() => _userRepositoryMock.Verify(x => x.Get(It.IsAny<string>()), Times.AtLeast(1)));
			Assert.DoesNotThrow(() => _messageRepositoryMock.Verify(x => x.Get(), Times.AtLeast(1)));
			Assert.DoesNotThrow(() => _consoleMock.Verify(x => x.WriteLine(It.IsAny<string>(), It.IsAny<object[]>()), Times.Exactly(2)));
			Assert.IsTrue(_consoleMock.WriteLineBuffer.Count == 2);
			Assert.AreEqual("User1 - Test1 (2 minutes ago)", _consoleMock.WriteLineBuffer[0]);
			Assert.AreEqual("User2 - Test2 (1 minutes ago)", _consoleMock.WriteLineBuffer[1]);
		}

		[Test()]
		public void CanHandleTest()
		{
			string username1;
			Assert.IsTrue(_wallCommandHandler.CanHandle("User1 wall", out username1));
			Assert.AreEqual("User1", username1);

			Assert.IsFalse(_wallCommandHandler.CanHandle("User1 -> Posting something", out username1));
			Assert.IsFalse(_wallCommandHandler.CanHandle("User1 follows User2", out username1));
			Assert.IsFalse(_wallCommandHandler.CanHandle("User1", out username1));

		}
	}
}