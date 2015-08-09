using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using Codurance.Business.Model.Entities;

namespace Codurance.Repository.SQLCE.Context
{
	internal sealed class UserConfig : EntityTypeConfiguration<User>
	{
		public UserConfig()
		{
			HasKey(x => x.Id);
			Property(x => x.Id).HasColumnName("Id");
			Property(x => x.Username).HasColumnName("Username")
				.HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UK_User_Username", 1) { IsUnique = true }));

			HasMany(x => x.Messages).WithRequired(x => x.User).HasForeignKey(x => x.UserId).WillCascadeOnDelete(true);
			HasMany(x => x.Following).WithMany(x => x.Followed).Map(x => x.ToTable("Follows").MapLeftKey("FollowerUserId").MapRightKey("FollowedUserId"));
		}
	}
}