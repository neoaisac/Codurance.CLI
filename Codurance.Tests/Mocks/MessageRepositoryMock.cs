using System;
using System.Collections.Generic;
using System.Linq;
using Codurance.Business.Model.Entities;
using Codurance.Business.Model.Repositories;
using Codurance.Shared.Extensions;
using Moq;

namespace Codurance.Tests.Mocks
{
	public class MessageRepositoryMock : Mock<IMessageRepository>
	{
		public List<Message> CachedMessages { get; set; }

		public MessageRepositoryMock()
		{
			CachedMessages = new List<Message>();

			Setup(x => x.Create(It.IsAny<Message>())).Returns((Message x) => x.AsTask());
			Setup(x => x.Delete(It.IsAny<Message>())).Returns((Message x) => true.AsTask());
			Setup(x => x.Update(It.IsAny<Message>())).Returns((Message x) => x.AsTask());

			// Default Get uses CachedMessages (set during the tests)
			Setup(x => x.Get()).Returns(() => CachedMessages.AsEnumerable().AsTask());
			Setup(x => x.Get(It.IsAny<Guid>())).Returns((Guid x) => CachedMessages.Where(m => m.UserId == x).AsTask());
		}
	}
}