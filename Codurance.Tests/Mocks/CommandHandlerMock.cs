using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codurance.Business.Model.Contracts;
using Codurance.Shared.Extensions;
using Moq;

namespace Codurance.Tests.Mocks
{
	internal class CommandHandlerMock : Mock<ICommandHandler>
	{
		public List<string> ErrorsBuffer { get; set; }
		public CommandHandlerMock(bool handleResult, string error = null)
		{
			if (!string.IsNullOrWhiteSpace(error)) ErrorsBuffer = new List<string>() { error };

			Setup(x => x.Handle(It.IsAny<string>())).Returns((string x) => handleResult.AsTask());
			Setup(x => x.Errors).Returns(ErrorsBuffer);
		}
	}
}
