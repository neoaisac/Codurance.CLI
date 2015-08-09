using System.Data.Common;
using System.Data.Entity;

namespace Codurance.Repository.SQLCE.Context
{
	internal class CoduranceDbContext : DbContext
	{
		public CoduranceDbContext(DbConnection connection, bool contextOwnsConnection)
			: base(connection, contextOwnsConnection)
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new MessageConfig());
			modelBuilder.Configurations.Add(new UserConfig());

			base.OnModelCreating(modelBuilder);
		}
	}
}