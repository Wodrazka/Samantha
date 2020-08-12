using System;
using System.Collections.Generic;

namespace DependencyInjection
{
    public interface IRegistrationCollection
    {

        void Add(IRegistration registration);

        IRegistrationCollection Where(Func<IRegistration, bool> predicate);

        IRegistrationCollection AsImplementedInterfaces();

        IRegistrationCollection Single();
    }
}