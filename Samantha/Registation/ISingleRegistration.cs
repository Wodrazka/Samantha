namespace Samantha.Registation;

using System;
using System.Collections.Generic;

public interface ISingleRegistration : IRegistration
{
    public RegistrationSettings RegistrationSettings { get; set; }

    public List<Type> AsTypes { get; }

    public Type ConstructionType { get; }

    public Func<IContainer, Type, object> CreateFunction { get; set; }

    public ISingleRegistration AsSelf();

    public ISingleRegistration AsType<T>();

    public ISingleRegistration AsType(params Type[] types);

    public ISingleRegistration AsImplementedInterfaces();

    public ISingleRegistration PerRequest();

    public ISingleRegistration PerInstance();
}
