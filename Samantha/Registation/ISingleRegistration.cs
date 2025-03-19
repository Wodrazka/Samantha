namespace Samantha.Registation;

using System;
using System.Collections.Generic;

public interface ISingleRegistration : IRegistration
{
    RegistrationSettings RegistrationSettings { get; set; }

    List<Type> AsTypes { get; }

    Type ConstructionType { get; }

    Func<IContainer, Type, object> CreateFunction { get; set; }

    ISingleRegistration AsSelf();

    ISingleRegistration AsType<T>();

    ISingleRegistration AsType(params Type[] types);

    ISingleRegistration AsImplementedInterfaces();

    ISingleRegistration PerRequest();

    ISingleRegistration PerInstance();
}
