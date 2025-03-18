using System;
using Samantha.Registation;

namespace Samantha
{
    public interface ICollectionRegistration : IRegistration
    {
        void Add(ISingleRegistration registration);

        ICollectionRegistration Where(Func<ISingleRegistration, bool> predicate);

        ICollectionRegistration AsImplementedInterfaces();

        ICollectionRegistration Single();

        ICollectionRegistration Except<T>();

        ICollectionRegistration Except<T>(Action<IRegistration> except);
    }
}