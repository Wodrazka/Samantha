using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Registation
{
    public class RegistrationCollection : IRegistrationCollection
    {

        private List<IRegistration> _registrations;

        public RegistrationCollection()
        {
            _registrations = new List<IRegistration>();
        }

        public void Add(IRegistration registration)
        {
            _registrations.Add(registration);
        }

        public IEnumerable<IRegistration> GetRegistrations()
        {
            return _registrations;
        }

        public IRegistrationCollection Where(Func<IRegistration, bool> predicate)
        {
            _registrations.RemoveAll(e => !predicate(e));
            return this;
        }

        public IRegistrationCollection AsImplementedInterfaces()
        {
            foreach (var reg in _registrations)
                reg.AsImplementedInterfaces();

            return this;
        }

        public IRegistrationCollection Single()
        {
            foreach (var reg in _registrations)
                reg.Single();

            return this;
        }
    }
}
