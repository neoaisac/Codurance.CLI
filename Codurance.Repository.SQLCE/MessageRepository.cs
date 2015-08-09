using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codurance.Business.Model.Entities;
using Codurance.Business.Model.Repositories;
using Codurance.Repository.SQLCE.Base;
using Codurance.Shared.Extensions;

namespace Codurance.Repository.SQLCE
{
	public class MessageRepository : SQLCERepositoryBase<Message>, IMessageRepository
	{
		public MessageRepository(
			ICoduranceRepositoryContext context)
			: base(context)
		{ }

		public Task<IEnumerable<Message>> Get(Guid userId)
		{
			return Context.GetCollection<Message>().Where(x => x.UserId == userId).AsTask();
		}
	}
}