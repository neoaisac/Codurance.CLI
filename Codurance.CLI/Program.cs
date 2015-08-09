using System;
using Microsoft.Practices.Unity;

namespace Codurance.CLI
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			int result = -1;
			using (var unityContainer = new UnityContainer())
			{
				Init.UnityConfig.Initialize(unityContainer);

				var application = unityContainer.Resolve<Application>();
				result = application.Run(args).Result;
			}

			Environment.Exit(result);
		}
	}
}