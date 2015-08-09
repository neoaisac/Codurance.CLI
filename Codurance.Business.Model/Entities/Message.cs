using System;

namespace Codurance.Business.Model.Entities
{
	public class Message
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Content { get; set; }
		public DateTime TimeStamp { get; set; }

		public User User { get; set; }
	}
}