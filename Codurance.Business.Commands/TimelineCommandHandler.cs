using System;
using System.Linq;
using System.Threading.Tasks;
using Codurance.Business.Commands.Base;
using Codurance.Business.Model.Repositories;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Commands
{
	public class TimelineCommandHandler : CommandHandlerBase
	{
		private readonly IUserRepository _userRepository;

		public TimelineCommandHandler(
			IConsole console,
			IUserRepository userRepository)
			: base(console)
		{
			_userRepository = userRepository;
		}

		public override async Task<bool> Handle(string element)
		{
			string username;
			if (!CanHandle(element, out username))
				return false;

			// Handle
			var user = await _userRepository.Get(username);
			if (user == null)
			{
				Errors.Add("User not found.");
				return true;
			}

			if (user.Messages == null || !user.Messages.Any())
			{
				Errors.Add("No messages to show.");
				return true;
			}

			foreach (var message in user.Messages.OrderBy(x => x.TimeStamp))
			{
				Console.WriteLine("{0} ({1} minutes ago)",
					message.Content,
					(DateTime.UtcNow - message.TimeStamp).Minutes);
			}

			return true;
		}

		public bool CanHandle(string element, out string username)
		{
			username = null;

			var parts = element.Split(' ');

			// Handle commands of type:
			// [user name]
			if (parts.Length == 1)
			{
				username = parts[0];

				return true;
			}

			return false;
		}
	}
}