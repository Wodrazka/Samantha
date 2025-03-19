namespace Samantha;

using System;
using System.Collections.Generic;
using Samantha.Registation;

public class Registration : ISingleRegistration
{

    public RegistrationSettings RegistrationSettings { get; set; }

    public List<Type> AsTypes { get; internal set; }

    public Type ConstructionType { get; internal set; }

    public Func<IContainer, Type, object> CreateFunction { get; set; }

    public Registration() => AsTypes = [];

    public ISingleRegistration AsSelf()
    {
        AsTypes.Add(ConstructionType);
        return this;
    }

    public ISingleRegistration AsType<T>()
    {
        AsTypes.Add(typeof(T));
        return this;
    }

    public ISingleRegistration AsType(params Type[] types)
    {
        foreach (var type in types)
        {
            AsTypes.Add(type);
        }

        return this;
    }

    public ISingleRegistration AsImplementedInterfaces() => AsType(ConstructionType.GetInterfaces());

    public ISingleRegistration PerRequest()
    {
        RegistrationSettings.Scope = Scope.PerRequest;
        return this;
    }

    public ISingleRegistration PerInstance()
    {
        RegistrationSettings.Scope = Scope.Instance;
        return this;
    }
}
