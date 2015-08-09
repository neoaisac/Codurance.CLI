using System;
using System.Collections.Generic;

namespace Codurance.Business.Model.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Username { get; set; }

		public virtual ICollection<Message> Messages { get; set; }
		public virtual ICollection<User> Following { get; set; }
		public virtual ICollection<User> Followed { get; set; }
	}
}