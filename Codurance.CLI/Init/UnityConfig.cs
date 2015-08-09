using Codurance.Business.Commands;
using Codurance.Business.Model.Contracts;
using Codurance.Business.Model.Repositories;
using Codurance.Repository.SQLCE;
using Codurance.Shared.Contracts;
using Codurance.Shared.Default;
using Microsoft.Practices.Unity;

namespace Codurance.CLI.Init
{
	public static class UnityConfig
	{
		public static void Initialize(IUnityContainer container)
		{
			container.RegisterType<ICoduranceRepositoryContext, CoduranceRepositoryContext>(new HierarchicalLifetimeManager());

			container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IMessageRepository, MessageRepository>(new HierarchicalLifetimeManager());

			container.RegisterType<ICommandHandlerChain, MainCommandHandlerChain>(new HierarchicalLifetimeManager());
			container.RegisterType<ICommandHandler, PostCommandHandler>(typeof(PostCommandHandler).Name, new HierarchicalLifetimeManager());
			container.RegisterType<ICommandHandler, FollowCommandHandler>(typeof(FollowCommandHandler).Name, new HierarchicalLifetimeManager());
			container.RegisterType<ICommandHandler, WallCommandHandler>(typeof(WallCommandHandler).Name, new HierarchicalLifetimeManager());
			container.RegisterType<ICommandHandler, TimelineCommandHandler>(typeof(TimelineCommandHandler).Name, new HierarchicalLifetimeManager());

			container.RegisterType<IConsole, DefaultConsole>(new HierarchicalLifetimeManager());
		}
	}
}