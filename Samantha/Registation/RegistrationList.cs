namespace Samantha.Registation;

using System;
using System.Collections.Generic;

public class RegistrationList : IRegistrationList
{

    private readonly List<ISingleRegistration> _registrations;

    public RegistrationList() => _registrations = [];

    public void Add(ISingleRegistration registration) => _registrations.Add(registration);

    public IEnumerable<ISingleRegistration> GetRegistrations() => _registrations;

    public IRegistrationList Where(Func<ISingleRegistration, bool> predicate)
    {
        _registrations.RemoveAll(e => !predicate(e));
        return this;
    }

    public IRegistrationList AsImplementedInterfaces()
    {
        foreach (var reg in _registrations)
        {
            reg.AsImplementedInterfaces();
        }

        return this;
    }

    public IRegistrationList PerInstance()
    {
        foreach (var reg in _registrations)
        {
            reg.PerInstance();
        }

        return this;
    }

    public IRegistrationList Except<T>()
    {
        var registration = _registrations.Find(r => r.ConstructionType == typeof(T));
        _registrations.Remove(registration);

        return this;
    }

    public IRegistrationList Except<T>(Action<IRegistration> except)
    {
        var registration = _registrations.Find(r => r.ConstructionType == typeof(T));
        except(registration);

        return this;
    }
}
