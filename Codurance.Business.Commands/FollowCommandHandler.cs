using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codurance.Business.Commands.Base;
using Codurance.Business.Model.Entities;
using Codurance.Business.Model.Repositories;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Commands
{
	public class FollowCommandHandler : CommandHandlerBase
	{
		private readonly IUserRepository _userRepository;

		public FollowCommandHandler(
			IConsole console,
			IUserRepository userRepository)
			: base(console)
		{
			_userRepository = userRepository;
		}

		public override async Task<bool> Handle(string element)
		{
			string follower, followed;
			if (!CanHandle(element, out follower, out followed))
				return false;

			// Handle
			// Self-follows do nothing
			if (follower == followed)
			{
				Errors.Add("You cannot follow yourself.");
				return true;
			}

			// Get users
			var userFollower = await _userRepository.Get(follower);
			var userFollowed = await _userRepository.Get(followed);

			// If any user does not exist do nothing
			if (userFollower == null || userFollowed == null)
			{
				Errors.Add("One of the users is unknown.");
				return true;
			}

			// If follower already follows folowed do nothing
			if (userFollower.Following != null && userFollower.Following.Any(x => x.Username == followed))
			{
				return true;
			}

			// Otherwise, add a follow
			if (userFollower.Following == null) userFollower.Following = new List<User>();
			userFollower.Following.Add(userFollowed);
			userFollower = await _userRepository.Update(userFollower);

			return true;
		}

		public bool CanHandle(string element, out string follower, out string followed)
		{
			follower = null;
			followed = null;

			var parts = element.Split(' ');

			// Handle commands of type:
			// [user name] follows [user name]
			if (parts.Length == 3 && parts[1] == "follows")
			{
				follower = parts[0];
				followed = parts[2];

				return true;
			}

			return false;
		}
	}
}