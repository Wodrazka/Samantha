using DependencyInjection.Binding;
using DependencyInjection.Registation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DependencyInjection
{
    public class ContainerBuilder : IContainerBuilder
    {

        private List<IRegistration> _registrations;
        private List<IRegistrationCollection> _registrationCollections;

        private bool _shouldRegisterSelf = false;

        public ContainerBuilder()
        {
            _registrations = new List<IRegistration>();
            _registrationCollections = new List<IRegistrationCollection>();
        }

        public IContainer Build()
        {
            Container container = new Container();

            BuildRegisrations(container);
            BuildCollections(container);

            if (_shouldRegisterSelf)
                container.AddBinding(typeof(IContainer), new SingletonBinding(container));

            return container;
        }

        private void BuildRegisrations(Container container)
        {
            foreach (var reg in _registrations)
            {
                IBinding binding = null;

                switch (reg)
                {
                    case DynamicRegistration dynamicRegistration:
                        binding = new DynamicBinding()
                        {
                            ConstructionType = reg.ConstructionType,
                            Scope = reg.RegistrationSettings.Scope
                        };
                        break;
                    case SingletonRegistration singleton:
                        binding = new SingletonBinding(singleton.Value);
                        break;
                }

                if (reg.AsTypes.Count == 0)
                    container.AddBinding(reg.ConstructionType, binding);

                foreach (var type in reg.AsTypes)
                {
                    container.AddBinding(type, binding);
                }
            }
        }

        private void BuildCollections(Container container)
        {
            foreach(var col in _registrationCollections)
            {
                foreach (var reg in ((RegistrationCollection)col).GetRegistrations())
                {
                    IBinding binding = null;

                    switch (reg)
                    {
                        case DynamicRegistration dynamicRegistration:
                            binding = new DynamicBinding()
                            {
                                ConstructionType = reg.ConstructionType,
                                Scope = reg.RegistrationSettings.Scope
                            };
                            break;
                        case SingletonRegistration singleton:
                            binding = new SingletonBinding(singleton.Value);
                            break;
                    }

                    if (reg.AsTypes.Count == 0)
                        container.AddBinding(reg.ConstructionType, binding);

                    foreach (var type in reg.AsTypes)
                    {
                        container.AddBinding(type, binding);
                    }
                }
            }
        }

        public void RegisterSelf()
        {
            _shouldRegisterSelf = true;
        }

        public IRegistration Register<T>()
        {
            IRegistration registration = new DynamicRegistration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    Scope = Scope.PerRequest,
                },
                ConstructionType = typeof(T)
            };

            _registrations.Add(registration);

            return registration;
        }

        public IRegistration RegisterSingleton<T>(T singleton)
        {
            IRegistration registration = new SingletonRegistration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    
                },
                ConstructionType = typeof(T),
                Value = singleton
            };

            _registrations.Add(registration);

            return registration;
        }

        public IRegistrationCollection RegisterAssemplyTypes(Assembly assembly)
        {
            IRegistrationCollection result = new RegistrationCollection();

            foreach(var t in assembly.GetTypes().Where(t => t.IsClass))
            {
                result.Add(new DynamicRegistration()
                {
                    RegistrationSettings = new RegistrationSettings()
                    {
                        Scope = Scope.PerRequest,
                    },
                    ConstructionType = t
                });
            }

            _registrationCollections.Add(result);
            return result;
        }
    }
}
