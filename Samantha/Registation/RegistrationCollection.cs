using System;
using System.Collections.Generic;

namespace Samantha.Registation
{
    public class RegistrationCollection : ICollectionRegistration
    {

        private List<ISingleRegistration> _registrations;

        public RegistrationCollection()
        {
            _registrations = new List<ISingleRegistration>();
        }

        public void Add(ISingleRegistration registration)
        {
            _registrations.Add(registration);
        }

        public IEnumerable<ISingleRegistration> GetRegistrations()
        {
            return _registrations;
        }

        public ICollectionRegistration Where(Func<ISingleRegistration, bool> predicate)
        {
            _registrations.RemoveAll(e => !predicate(e));
            return this;
        }

        public ICollectionRegistration AsImplementedInterfaces()
        {
            foreach (var reg in _registrations)
                reg.AsImplementedInterfaces();

            return this;
        }

        public ICollectionRegistration Single()
        {
            foreach (var reg in _registrations)
                reg.PerInstance();

            return this;
        }

        public ICollectionRegistration Except<T>()
        {
            var registration = _registrations.Find(r => r.ConstructionType == typeof(T));
            _registrations.Remove(registration);

            return this;
        }

        public ICollectionRegistration Except<T>(Action<IRegistration> except)
        {
            var registration = _registrations.Find(r => r.ConstructionType == typeof(T));
            except(registration);

            return this;
        }
    }
}
