using DependencyInjection;
using System;
using System.Reflection;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

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
