using DependencyInjection.Test.Classes;
using Xunit;

namespace DependencyInjection.Test
{
    public class ContainerBuilderTests
    {

        [Fact]
        public void SingletonRegistration()
        {
            var cb = new ContainerBuilder();
            Person p = null;

            cb.RegisterSingleton(new Person { Name = "singleton" }).AsSelf();
            var container = cb.Build();

            try
            {
                p = container.Resolve<Person>();
            }
            catch
            {

            }

            Assert.True(p?.Name == "singleton");
        }

        [Fact]
        public void TypeRegistration()
        {
            var cb = new ContainerBuilder();
            Person p = null;

            cb.Register<Person>().AsSelf();
            var container = cb.Build();

            try
            {
                p = container.Resolve<Person>();
            }
            catch
            {
                
            }

            Assert.True(p != null);
        }

        [Fact]
        public void TypeRegistrationPerContainer()
        {
            var cb = new ContainerBuilder();
            Person p1 = null;
            Person p2 = null;

            cb.Register<Person>().AsSelf().Single();
            var container = cb.Build();

            try
            {
                p1 = container.Resolve<Person>();
                p2 = container.Resolve<Person>();
            }
            catch
            {

            }

            Assert.True(p1?.Name == p2.Name);
        }

        [Fact]
        public void TypeRegistrationPerRequest()
        {
            var cb = new ContainerBuilder();
            Person p1 = null;
            Person p2 = null;

            cb.Register<Person>().AsSelf();
            var container = cb.Build();

            try
            {
                p1 = container.Resolve<Person>();
                p2 = container.Resolve<Person>();
            }
            catch
            {

            }

            Assert.True(p1?.Name != p2.Name);
        }
    }
}
