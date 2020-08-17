using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Registation
{
    public interface ISingleRegistration : IRegistration
    {
        RegistrationSettings RegistrationSettings { get; set; }

        List<Type> AsTypes { get; }

        Type ConstructionType { get; }

        ISingleRegistration AsSelf();

        ISingleRegistration As<T>();

        ISingleRegistration As(params Type[] types);

        ISingleRegistration AsImplementedInterfaces();

        ISingleRegistration PerRequest();

        ISingleRegistration Single();
    }
}
