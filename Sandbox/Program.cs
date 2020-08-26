using Samantha;
using System;
using System.Reflection;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainerBuilder containerBuilder = new ContainerBuilder();

            containerBuilder.Register<string>((c, t) => "TeseT");

            containerBuilder.Register<User>((c, t) => new User()
            {
                Name = c.Resolve<string>()
            }).As<IUser>();

            containerBuilder.Register<ItemData>();
            containerBuilder.Register<UserData>();

            containerBuilder.RegisterAssemplyTypes(Assembly.GetExecutingAssembly())
                .Where(e => e.ConstructionType.Name.EndsWith("View"));

            containerBuilder.RegisterSelf();

            IContainer container = containerBuilder.Build();

            container.Resolve<ShellView>().Start();
            
        }
    }
}
