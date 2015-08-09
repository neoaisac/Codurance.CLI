using System;
using System.Linq;
using System.Threading.Tasks;
using Codurance.Business.Commands.Base;
using Codurance.Business.Model.Entities;
using Codurance.Business.Model.Repositories;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Commands
{
	public class PostCommandHandler : CommandHandlerBase
	{
		private readonly IMessageRepository _messageRepository;
		private readonly IUserRepository _userRepository;

		public PostCommandHandler(
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
			string username, content;
			if (!CanHandle(element, out username, out content))
				return false;

			// Handle
			var user = await _userRepository.Get(username);
			if (user == null)
			{
				user = new User()
				{
					Id = Guid.NewGuid(),
					Username = username
				};
				user = await _userRepository.Create(user);

				if (user == null)
				{
					Errors.Add("The user could not be created.");
					return true;
				}
			}

			var message = new Message()
			{
				Id = Guid.NewGuid(),
				TimeStamp = DateTime.UtcNow,
				UserId = user.Id,
				Content = content
			};
			message = await _messageRepository.Create(message);

			if (message == null)
			{
				Errors.Add("The message could not be created.");
				return true;
			}

			return true;
		}

		public bool CanHandle(string element, out string username, out string message)
		{
			username = null;
			message = null;

			var parts = element.Split(' ');

			// Handle commands of type:
			// [user name] -> [message]
			if (parts.Length >= 3 && parts[1] == "->")
			{
				username = parts[0];
				message = string.Join(" ", parts.Skip(2));

				return true;
			}

			return false;
		}
	}
}