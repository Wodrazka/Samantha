using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependencyInjection.Binding
{
    public class DynamicBinding : IBinding
    {

        private object _lastValue;

        public Type ConstructionType { get; set; }

        public Scope Scope { get; set; }

        internal Func<Type, object> GetBinding;

        public object Get()
        {
            if (Scope == Scope.PerRequest || _lastValue == null)
            {

                var constructors = ConstructionType.GetConstructors().ToList();

                constructors.Sort((e1, e2) => e2.GetParameters().Length.CompareTo(e1.GetParameters().Length));

                foreach (var ctor in constructors)
                {
                    var parameters = ctor.GetParameters();

                    bool invalid = false;
                    List<object> constructedParameters = new List<object>();

                    foreach (var p in parameters)
                    {
                        try
                        {
                            constructedParameters.Add(GetBinding(p.ParameterType));
                        }
                        catch
                        {
                            invalid = true;
                            break;
                        }
                    }

                    if (invalid)
                        break;

                    _lastValue = ctor.Invoke(constructedParameters.ToArray());
                    return _lastValue;
                }
            }

            return _lastValue;
        }
    }
}
