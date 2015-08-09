using NUnit.Framework;
using Codurance.Business.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codurance.Tests.Mocks;
using Moq;

namespace Codurance.Business.Commands.Tests
{
	[TestFixture()]
	public class TimelineCommandHandlerTests
	{
		private ConsoleMock _consoleMock;
		private UserRepositoryMock _userRepositoryMock;

		private TimelineCommandHandler _timelineCommandHandler;

		[SetUp]
		public void Setup()
		{
			_consoleMock = new ConsoleMock();
			_userRepositoryMock = new UserRepositoryMock();

			_timelineCommandHandler = new TimelineCommandHandler(_consoleMock.Object, _userRepositoryMock.Object);
		}

		[Test()]
		public void HandleTest()
		{
			_userRepositoryMock.CachedUsers.Add(new Model.Entities.User() { Id = Guid.NewGuid(), Username = "User1" });

			Assert.AreEqual(true, _timelineCommandHandler.Handle("User1").Result);
			Assert.DoesNotThrow(() => _userRepositoryMock.Verify(x => x.Get(It.IsAny<string>()), Times.AtLeast(1)));
		}

		[Test()]
		public void CanHandleTest()
		{
			string username1;
			Assert.IsTrue(_timelineCommandHandler.CanHandle("User1", out username1));
			Assert.AreEqual("User1", username1);

			Assert.IsFalse(_timelineCommandHandler.CanHandle("User1 -> Posting something", out username1));
			Assert.IsFalse(_timelineCommandHandler.CanHandle("User1 wall", out username1));
			Assert.IsFalse(_timelineCommandHandler.CanHandle("User1 follows User2", out username1));
		}
	}
}