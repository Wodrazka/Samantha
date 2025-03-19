namespace Samantha;

using System;
using Samantha.Registation;

public interface IRegistrationList : IRegistration
{
    public void Add(ISingleRegistration registration);

    public IRegistrationList Where(Func<ISingleRegistration, bool> predicate);

    public IRegistrationList AsImplementedInterfaces();

    public IRegistrationList PerInstance();

    public IRegistrationList Except<T>();

    public IRegistrationList Except<T>(Action<IRegistration> except);
}
