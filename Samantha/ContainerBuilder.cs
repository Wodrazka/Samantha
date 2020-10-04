using Samantha.Binding;
using Samantha.Registation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Samantha
{
    public class ContainerBuilder : IContainerBuilder
    {

        private readonly List<IRegistration> _registrations;

        private bool _shouldRegisterSelf = false;

        public ContainerBuilder()
        {
            _registrations = new List<IRegistration>();
        }

        public IContainer Build()
        {
            Container container = new Container();

            BuildRegisrations(container);

            if (_shouldRegisterSelf)
                container.AddBinding(typeof(IContainer), new FunctionBinding(container)
                {
                    ConstructionType = typeof(IContainer),
                    Scope = Scope.Instance,
                    Function = (c, t) => container,
                });

            return container;
        }

        private void BuildRegistration(Container container, ISingleRegistration registration)
        {
            IBinding binding = new FunctionBinding(container)
            {
                Function = registration.Function,
                Scope = registration.RegistrationSettings.Scope,
                ConstructionType = registration.ConstructionType,
                IsGeneric = registration.RegistrationSettings.IsGeneric
            };

            if (registration.AsTypes.Count == 0)
                container.AddBinding(registration.ConstructionType, binding);

            foreach (var type in registration.AsTypes)
            {
                container.AddBinding(type, binding);
            }
        }

        private void BuildRegisrations(Container container)
        {
            foreach (var reg in _registrations)
            {
                if (reg is ISingleRegistration single)
                {
                    BuildRegistration(container, single);
                }
                else if (reg is ICollectionRegistration collection)
                {
                    foreach (var colRegistration in ((RegistrationCollection)collection).GetRegistrations())
                    {
                        BuildRegistration(container, colRegistration);
                    }
                }
            }
        }

        public void RegisterSelf()
        {
            _shouldRegisterSelf = true;
        }

        public ISingleRegistration Register<T>()
        {
            ISingleRegistration registration = new Registration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    Scope = Scope.PerRequest,
                },
                ConstructionType = typeof(T),
                Function = (c, t) => Functions.Create(c, t)
            };

            _registrations.Add(registration);

            return registration;
        }

        public ISingleRegistration Register(Type type)
        {
            ISingleRegistration registration = new Registration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    Scope = Scope.PerRequest,
                },
                ConstructionType = type,
                Function = (c, t) => Functions.Create(c, t)
            };

            _registrations.Add(registration);

            return registration;
        }

        public ISingleRegistration Register<T>(Func<IContainer, Type, object> func)
        {
            ISingleRegistration registration = new Registration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    Scope = Scope.PerRequest,
                },
                ConstructionType = typeof(T),
                Function = func
            };

            _registrations.Add(registration);

            return registration;
        }

        public ISingleRegistration RegisterSingleton<T>(T singleton)
        {
            ISingleRegistration registration = new Registration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    Scope = Scope.Instance
                },
                ConstructionType = typeof(T),
                Function = (c, t) => singleton
            };

            _registrations.Add(registration);

            return registration;
        }

        public ISingleRegistration RegisterGeneric(Type generic)
        {
            ISingleRegistration registration = new Registration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    Scope = Scope.PerRequest,
                    IsGeneric = true
                },
                ConstructionType = generic,
                Function = (c, t) => Functions.Create(c, t)
            };

            _registrations.Add(registration);

            return registration;
        }

        public ICollectionRegistration RegisterAssemplyTypes(Assembly assembly)
        {
            ICollectionRegistration result = new RegistrationCollection();

            foreach (var type in assembly.GetTypes().Where(t => t.IsClass))
            {
                result.Add(new Registration()
                {
                    RegistrationSettings = new RegistrationSettings()
                    {
                        Scope = Scope.PerRequest,
                    },
                    ConstructionType = type,
                    Function = (c, t) => Functions.Create(c, t)
                });
            }

            _registrations.Add(result);
            return result;
        }
    }
}
