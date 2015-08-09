using System;
using Codurance.Tests.Mocks;
using Moq;
using NUnit.Framework;

namespace Codurance.Business.Commands.Tests
{
	[TestFixture()]
	public class FollowCommandHandlerTests
	{
		private ConsoleMock _consoleMock;
		private UserRepositoryMock _userRepositoryMock;

		private FollowCommandHandler _followCommandHandler;

		[SetUp]
		public void Setup()
		{
			_consoleMock = new ConsoleMock();
			_userRepositoryMock = new UserRepositoryMock();

			_followCommandHandler = new FollowCommandHandler(_consoleMock.Object, _userRepositoryMock.Object);
		}

		[Test()]
		public void HandleTest()
		{
			_userRepositoryMock.CachedUsers.Add(new Model.Entities.User() { Id = Guid.NewGuid(), Username = "User1" });
			_userRepositoryMock.CachedUsers.Add(new Model.Entities.User() { Id = Guid.NewGuid(), Username = "User2" });

			Assert.AreEqual(true, _followCommandHandler.Handle("User1 follows User2").Result);
			Assert.DoesNotThrow(() => _userRepositoryMock.Verify(x => x.Get(It.IsAny<string>()), Times.AtLeast(2)));
		}

		[Test()]
		public void CanHandleTest()
		{
			string username1, username2;
			Assert.IsTrue(_followCommandHandler.CanHandle("User1 follows User2", out username1, out username2));
			Assert.AreEqual("User1", username1);
			Assert.AreEqual("User2", username2);

			Assert.IsFalse(_followCommandHandler.CanHandle("User1", out username1, out username2));
			Assert.IsFalse(_followCommandHandler.CanHandle("User1 -> Posting something", out username1, out username2));
			Assert.IsFalse(_followCommandHandler.CanHandle("User1 wall", out username1, out username2));
		}
	}
}