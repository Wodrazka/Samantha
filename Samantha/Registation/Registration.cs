using Samantha.Registation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samantha
{
    public class Registration : ISingleRegistration
    {

        public RegistrationSettings RegistrationSettings { get; set; }

        public List<Type> AsTypes { get; internal set; }

        public Type ConstructionType { get; internal set; }

        public Func<IContainer, Type, object> Function { get; set; }

        public Registration()
        {
            AsTypes = new List<Type>();
        }

        public ISingleRegistration AsSelf()
        {
            AsTypes.Add(ConstructionType);
            return this;
        }

        public ISingleRegistration As<T>()
        {
            AsTypes.Add(typeof(T));
            return this;
        }

        public ISingleRegistration As(params Type[] types)
        {
            foreach (var type in types)
                AsTypes.Add(type);

            return this;
        }

        public ISingleRegistration AsImplementedInterfaces()
        {
            return this.As(ConstructionType.GetInterfaces());
        }

        public ISingleRegistration PerRequest()
        {
            RegistrationSettings.Scope = Scope.PerRequest;
            return this;
        }

        public ISingleRegistration PerInstance()
        {
            RegistrationSettings.Scope = Scope.Instance;
            return this;
        }
    }
}
