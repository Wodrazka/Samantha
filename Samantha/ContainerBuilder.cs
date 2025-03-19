namespace Samantha;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Samantha.Binding;
using Samantha.Registation;

public class ContainerBuilder : IContainerBuilder
{

    private readonly List<IRegistration> _registrations;

    private bool _shouldRegisterSelf;

    public ContainerBuilder() => _registrations = [];

    public IContainer Build()
    {
        var container = new Container();

        BuildRegisrations(container);

        if (_shouldRegisterSelf)
        {
            container.AddBinding(typeof(IContainer), new FunctionBinding(container)
            {
                ConstructionType = typeof(IContainer),
                Scope = Scope.Instance,
                Function = (c, t) => container,
            });
        }

        return container;
    }

    private static void BuildRegistration(Container container, ISingleRegistration registration)
    {
        IBinding binding = new FunctionBinding(container)
        {
            Function = registration.CreateFunction,
            Scope = registration.RegistrationSettings.Scope,
            ConstructionType = registration.ConstructionType,
            IsGeneric = registration.RegistrationSettings.IsGeneric
        };

        if (registration.AsTypes.Count == 0)
        {
            container.AddBinding(registration.ConstructionType, binding);
        }

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
            else if (reg is IRegistrationList collection)
            {
                foreach (var colRegistration in ((RegistrationList)collection).GetRegistrations())
                {
                    BuildRegistration(container, colRegistration);
                }
            }
        }
    }

    public void RegisterSelf() => _shouldRegisterSelf = true;

    public ISingleRegistration Register<T>()
    {
        ISingleRegistration registration = new Registration()
        {
            RegistrationSettings = new RegistrationSettings()
            {
                Scope = Scope.PerRequest,
            },
            ConstructionType = typeof(T),
            CreateFunction = (c, t) => Functions.Create(c, t)
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
            CreateFunction = (c, t) => Functions.Create(c, t)
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
            CreateFunction = func
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
            CreateFunction = (c, t) => singleton
        };

        _registrations.Add(registration);

        return registration;
    }

    public ISingleRegistration RegisterGeneric(Type genericType)
    {
        ISingleRegistration registration = new Registration()
        {
            RegistrationSettings = new RegistrationSettings()
            {
                Scope = Scope.PerRequest,
                IsGeneric = true
            },
            ConstructionType = genericType,
            CreateFunction = (c, t) => Functions.Create(c, t)
        };

        _registrations.Add(registration);

        return registration;
    }

    public IRegistrationList RegisterAssemplyTypes(Assembly assembly)
    {
        var result = new RegistrationList();

        foreach (var type in assembly.GetTypes().Where(t => t.IsClass))
        {
            result.Add(new Registration()
            {
                RegistrationSettings = new RegistrationSettings()
                {
                    Scope = Scope.PerRequest,
                },
                ConstructionType = type,
                CreateFunction = (c, t) => Functions.Create(c, t)
            });
        }

        _registrations.Add(result);
        return result;
    }
}
