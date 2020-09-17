using Samantha.Test.Classes;
using Samantha.Test.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Samantha.Test
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
        public void TypeRegistrationPerInstance()
        {
            var cb = new ContainerBuilder();
            Person p1 = null;
            Person p2 = null;

            cb.Register<Person>().AsSelf().PerInstance();
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

        [Fact]
        public void TypeAsImplementedInterfaces()
        {
            var cb = new ContainerBuilder();
            IPerson p1 = null;
            IPerson p2 = null;

            cb.Register<Person>().AsImplementedInterfaces();
            var container = cb.Build();

            try
            {
                p1 = container.Resolve<IPerson>();
                p2 = container.Resolve<Person>();
            }
            catch
            {

            }

            Assert.True(p1 != null && p2 == null);
        }

        [Fact]
        public void RegisterAssemblies()
        {
            var cb = new ContainerBuilder();

            cb.RegisterAssemplyTypes(Assembly.GetExecutingAssembly());

            var container = cb.Build();

            foreach(var t in Assembly.GetExecutingAssembly().GetTypes().Where(e => e.IsClass))
            {
                object obj = null;

                try
                {
                    obj = container.Resolve(t);
                }
                catch (Exception)
                {
                    
                }

                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void RegisterAssembliesExcept()
        {
            var cb = new ContainerBuilder();

            cb.RegisterAssemplyTypes(Assembly.GetExecutingAssembly())
                .Except<Car>();

            var container = cb.Build();

            foreach (var t in Assembly.GetExecutingAssembly().GetTypes().Where(e => e.IsClass && e != typeof(Car)))
            {
                object obj = null;

                try
                {
                    obj = container.Resolve(t);
                }
                catch (Exception)
                {

                }

                Assert.NotNull(obj);
            }
        }
    }
}
