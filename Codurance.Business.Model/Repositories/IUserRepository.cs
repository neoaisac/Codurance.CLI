using System;
using System.Threading.Tasks;
using Codurance.Business.Model.Entities;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Model.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> Get(Guid userId);

		Task<User> Get(string username);
	}
}