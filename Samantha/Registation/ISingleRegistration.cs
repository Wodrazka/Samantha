using System;
using System.Collections.Generic;

namespace Samantha.Registation
{
    public interface ISingleRegistration : IRegistration
    {
        RegistrationSettings RegistrationSettings { get; set; }

        List<Type> AsTypes { get; }

        Type ConstructionType { get; }

        Func<IContainer, Type, object> Function { get; set; }

        ISingleRegistration AsSelf();

        ISingleRegistration As<T>();

        ISingleRegistration As(params Type[] types);

        ISingleRegistration AsImplementedInterfaces();

        ISingleRegistration PerRequest();

        ISingleRegistration PerInstance();
    }
}
