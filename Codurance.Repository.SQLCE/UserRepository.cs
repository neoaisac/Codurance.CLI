using System;
using System.Linq;
using System.Threading.Tasks;
using Codurance.Business.Model.Entities;
using Codurance.Business.Model.Repositories;
using Codurance.Repository.SQLCE.Base;
using Codurance.Shared.Extensions;

namespace Codurance.Repository.SQLCE
{
	public class UserRepository : SQLCERepositoryBase<User>, IUserRepository
	{
		public UserRepository(
			ICoduranceRepositoryContext context)
			: base(context)
		{ }

		public Task<User> Get(string username)
		{
			return Context.GetCollection<User>().FirstOrDefault(x => x.Username == username).AsTask();
		}

		public Task<User> Get(Guid userId)
		{
			return Context.GetCollection<User>().FirstOrDefault(x => x.Id == userId).AsTask();
		}
	}
}