using Samantha.Registation;
using System;
using System.Collections.Generic;

namespace Samantha
{
    public interface ICollectionRegistration : IRegistration
    {
        void Add(ISingleRegistration registration);

        ICollectionRegistration Where(Func<ISingleRegistration, bool> predicate);

        ICollectionRegistration AsImplementedInterfaces();

        ICollectionRegistration Single();
    }
}