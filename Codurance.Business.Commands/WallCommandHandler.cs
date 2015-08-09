using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codurance.Business.Commands.Base;
using Codurance.Business.Model.Repositories;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Commands
{
	public class WallCommandHandler : CommandHandlerBase
	{
		private readonly IMessageRepository _messageRepository;
		private readonly IUserRepository _userRepository;

		public WallCommandHandler(
			IConsole console,
			IUserRepository userRepository,
			IMessageRepository messageRepository)
			: base(console)
		{
			_userRepository = userRepository;
			_messageRepository = messageRepository;
		}

		public override async Task<bool> Handle(string element)
		{
			string username;
			if (!CanHandle(element, out username))
				return false;

			// Handle
			var user = await _userRepository.Get(username);

			// If user does not exist notify and return
			if (user == null)
			{
				Errors.Add("User does not exist.");
				return true;
			}

			var wallUserIds = new List<Guid>(user.Following.Select(x => x.Id));
			wallUserIds.Add(user.Id);
			var messages = (await _messageRepository.Get())
				.Join(wallUserIds, l => l.UserId, r => r, (l, r) => l)
				.OrderBy(x => x.TimeStamp);

			if (!messages.Any())
			{
				Errors.Add("No messages to show.");
				return true;
			}

			foreach (var message in messages)
			{
				Console.WriteLine("{0} - {1} ({2} minutes ago)",
					message.User.Username,
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
			// [user name] wall
			if (parts.Length == 2 && parts[1] == "wall")
			{
				username = parts[0];

				return true;
			}

			return false;
		}
	}
}