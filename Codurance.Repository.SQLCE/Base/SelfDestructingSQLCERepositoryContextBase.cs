using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.Threading.Tasks;
using Codurance.Repository.SQLCE.Context;
using Codurance.Shared.Contracts;
using Codurance.Shared.Extensions;

namespace Codurance.Repository.SQLCE.Base
{
	public abstract class SelfDestructingSQLCERepositoryContextBase : IRepositoryContext, IDisposable
	{
		private readonly DbContext _db;

		public SelfDestructingSQLCERepositoryContextBase()
		{
			_db = new CoduranceDbContext(new SqlCeConnection("Data Source=Codurance.sdf"), true);
			_db.Database.CreateIfNotExists();
		}

		public Task<T> Create<T>(T element)
			where T : class
		{
			return _db.Set<T>().Add(element).AsTask();
		}

		public Task<bool> Delete<T>(T element)
			where T : class
		{
			return (_db.Set<T>().Remove(element) != null).AsTask();
		}

		public void Dispose()
		{
			_db.Database.Delete();
			_db.Dispose();
		}

		public IEnumerable<T> GetCollection<T>()
			where T : class
		{
			return _db.Set<T>();
		}

		public Task<T> Update<T>(T element)
			where T : class
		{
			var entry = _db.Entry(element);
			entry.State = EntityState.Modified;
			return entry.Entity.AsTask();
		}

		async Task<int> IRepositoryContext.SaveChanges()
		{
			return await _db.SaveChangesAsync();
		}
	}
}