using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Registation
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
                reg.Single();

            return this;
        }
    }
}
