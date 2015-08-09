using System.Collections.Generic;
using Codurance.Shared.Contracts;

namespace Codurance.Business.Model.Contracts
{
	public interface ICommandHandler : IHandler<string>
	{
		IList<string> Errors { get; }
	}
}