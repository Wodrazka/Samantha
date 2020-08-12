using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{ 

    public interface IRegistration
    {

        RegistrationSettings RegistrationSettings { get; set; }

        List<Type> AsTypes { get; }

        Type ConstructionType { get; }

        IRegistration AsSelf();

        IRegistration As<T>();

        IRegistration As(params Type[] types);

        IRegistration AsImplementedInterfaces();

        IRegistration PerRequest();

        IRegistration Single();

    }
}
