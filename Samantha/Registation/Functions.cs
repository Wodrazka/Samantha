using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samantha.Registation
{
    public static class Functions
    {

        public static Func<IContainer, Type, object> Create = (c, t) =>
        {
            var constructors = t.GetConstructors().ToList();

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
                        constructedParameters.Add(c.Resolve(p.ParameterType));
                    }
                    catch
                    {
                        invalid = true;
                        break;
                    }
                }

                if (invalid)
                    break;

                return ctor.Invoke(constructedParameters.ToArray());
            }
            return null;
        };

    }
}
