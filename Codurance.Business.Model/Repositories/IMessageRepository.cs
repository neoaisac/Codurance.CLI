using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codurance.Business.Model.Entities;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Model.Repositories
{
	public interface IMessageRepository : IRepository<Message>
	{
		Task<IEnumerable<Message>> Get(Guid userId);
	}
}