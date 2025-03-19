namespace Samantha;

using System;
using Samantha.Registation;

public interface IRegistrationList : IRegistration
{
    void Add(ISingleRegistration registration);

    IRegistrationList Where(Func<ISingleRegistration, bool> predicate);

    IRegistrationList AsImplementedInterfaces();

    IRegistrationList PerInstance();

    IRegistrationList Except<T>();

    IRegistrationList Except<T>(Action<IRegistration> except);
}
