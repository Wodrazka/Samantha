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
                container.AddBinding(typeof(IContainer), new SingletonBinding(container));

            return container;
        }

        private void BuildRegisrations(Container container)
        {
            foreach (var reg in _registrations)
            {
                IBinding binding = null;

                if (reg is ISingleRegistration single)
                {
                    switch (single)
                    {
                        case DynamicRegistration dynamicRegistration:
                            binding = new DynamicBinding()
                            {
                                ConstructionType = dynamicRegistration.ConstructionType,
                                Scope = dynamicRegistration.RegistrationSettings.Scope
                            };
                            break;
                        case SingletonRegistration singleton:
                            binding = new SingletonBinding(singleton.Value);
                            break;
                    }

                    if (single.AsTypes.Count == 0)
                        container.AddBinding(single.ConstructionType, binding);

                    foreach (var type in single.AsTypes)
                    {
                        container.AddBinding(type, binding);
                    }
                }
                else if (reg is ICollectionRegistration collection)
                {
                    foreach (var colRegistration in ((RegistrationCollection)collection).GetRegistrations())
                    {
                        switch (colRegistration)
                        {
                            case DynamicRegistration dynamicRegistration:
                                binding = new DynamicBinding()
                                {
                                    ConstructionType = dynamicRegistration.ConstructionType,
                                    Scope = dynamicRegistration.RegistrationSettings.Scope
                                };
                                break;
                            case SingletonRegistration singleton:
                                binding = new SingletonBinding(singleton.Value);
                                break;
                        }

                        if (colRegistration.AsTypes.Count == 0)
                            container.AddBinding(colRegistration.ConstructionType, binding);

                        foreach (var type in colRegistration.AsTypes)
                        {
                            container.AddBinding(type, binding);
                        }
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
            ISingleRegistration registration = new DynamicRegistration()
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

        public ISingleRegistration RegisterSingleton<T>(T singleton)
        {
            ISingleRegistration registration = new SingletonRegistration()
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

        public ICollectionRegistration RegisterAssemplyTypes(Assembly assembly)
        {
            ICollectionRegistration result = new RegistrationCollection();

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

            _registrations.Add(result);
            return result;
        }
    }
}
