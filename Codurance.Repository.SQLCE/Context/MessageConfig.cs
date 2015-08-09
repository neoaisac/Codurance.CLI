using System.Data.Entity.ModelConfiguration;
using Codurance.Business.Model.Entities;

namespace Codurance.Repository.SQLCE.Context
{
	internal sealed class MessageConfig : EntityTypeConfiguration<Message>
	{
		public MessageConfig()
		{
			HasKey(x => x.Id);
			Property(x => x.Id).HasColumnName("Id");
			Property(x => x.UserId).HasColumnName("UserId").IsRequired();
			Property(x => x.TimeStamp).HasColumnName("TimeStamp").IsRequired();
			Property(x => x.Content).HasColumnName("Content").IsRequired();

			HasRequired(x => x.User).WithMany().HasForeignKey(x => x.UserId).WillCascadeOnDelete(true);
		}
	}
}