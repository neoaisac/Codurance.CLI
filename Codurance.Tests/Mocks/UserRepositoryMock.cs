using System;
using System.Collections.Generic;
using System.Linq;
using Codurance.Business.Model.Entities;
using Codurance.Business.Model.Repositories;
using Codurance.Shared.Extensions;
using Moq;

namespace Codurance.Tests.Mocks
{
	public class UserRepositoryMock : Mock<IUserRepository>
	{
		public List<User> CachedUsers { get; set; }

		public UserRepositoryMock()
		{
			CachedUsers = new List<User>();

			Setup(x => x.Create(It.IsAny<User>())).Returns((User x) => x.AsTask());
			Setup(x => x.Delete(It.IsAny<User>())).Returns((User x) => true.AsTask());
			Setup(x => x.Update(It.IsAny<User>())).Returns((User x) => x.AsTask());

			// Default Get uses CachedUsers (set during the tests)
			Setup(x => x.Get()).Returns(() => CachedUsers.AsEnumerable().AsTask());
			Setup(x => x.Get(It.IsAny<Guid>())).Returns((Guid x) => CachedUsers.FirstOrDefault(u => u.Id == x).AsTask());
			Setup(x => x.Get(It.IsAny<string>())).Returns((string x) => CachedUsers.FirstOrDefault(u => u.Username == x).AsTask());
		}
	}
}