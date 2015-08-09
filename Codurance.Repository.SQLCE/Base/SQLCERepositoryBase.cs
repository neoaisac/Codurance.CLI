using System.Collections.Generic;
using System.Threading.Tasks;
using Codurance.Business.Model.Repositories;
using Codurance.Shared.Contracts;
using Codurance.Shared.Extensions;

namespace Codurance.Repository.SQLCE.Base
{
	public abstract class SQLCERepositoryBase<T> : IRepository<T>
		where T : class
	{
		protected readonly ICoduranceRepositoryContext Context;

		public SQLCERepositoryBase(
			ICoduranceRepositoryContext context)
		{
			Context = context;
		}

		public async Task<T> Create(T element)
		{
			var result = await Context.Create(element);
			await Context.SaveChanges();
			return result;
		}

		public async Task<bool> Delete(T element)
		{
			var result = await Context.Delete(element);
			await Context.SaveChanges();
			return result;
		}

		public async Task<IEnumerable<T>> Get()
		{
			var result = await Context.GetCollection<T>().AsTask();
			await Context.SaveChanges();
			return result;
		}

		public async Task<T> Update(T element)
		{
			var result = await Context.Update(element);
			await Context.SaveChanges();
			return result;
		}
	}
}