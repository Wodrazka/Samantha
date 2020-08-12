using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependencyInjection
{
    public abstract class Registration : IRegistration
    {

        public RegistrationSettings RegistrationSettings { get; set; }

        public List<Type> AsTypes { get; internal set; }

        public Type ConstructionType { get; internal set; }

        public Registration()
        {
            AsTypes = new List<Type>();
        }

        public IRegistration AsSelf()
        {
            AsTypes.Add(ConstructionType);
            return this;
        }

        public IRegistration As<T>()
        {
            AsTypes.Add(typeof(T));
            return this;
        }

        public IRegistration As(params Type[] types)
        {
            foreach (var type in types)
                AsTypes.Add(type);

            return this;
        }

        public IRegistration AsImplementedInterfaces()
        {
            return this.As(ConstructionType.GetInterfaces());
        }

        public IRegistration PerRequest()
        {
            RegistrationSettings.Scope = Scope.PerRequest;
            return this;
        }

        public IRegistration Single()
        {
            RegistrationSettings.Scope = Scope.Instance;
            return this;
        }
    }
}
