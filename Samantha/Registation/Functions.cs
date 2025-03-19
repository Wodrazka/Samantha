namespace Samantha.Registation;

using System;
using System.Collections.Generic;
using System.Linq;

public static class Functions
{

    public static Func<IContainer, Type, object> Create => (c, t) =>
    {
        var constructors = t.GetConstructors().ToList();

        constructors.Sort((e1, e2) => e2.GetParameters().Length.CompareTo(e1.GetParameters().Length));

        foreach (var ctor in constructors)
        {
            var parameters = ctor.GetParameters();

            var invalid = false;
            var constructedParameters = new List<object>();

            foreach (var p in parameters)
            {
                try
                {
                    var constructedParameter = c.Resolve(p.ParameterType);
                    constructedParameters.Add(constructedParameter);
                }
                catch
                {
                    if (p.HasDefaultValue)
                    {
                        constructedParameters.Add(p.DefaultValue);
                    }
                    else
                    {
                        invalid = true;
                        break;
                    }
                }
            }

            if (invalid)
            {
                break;
            }

            return ctor.Invoke([.. constructedParameters]);
        }
        return null;
    };

    public static Func<IContainer, Type, object> CreateGeneric => (c, t) =>
    {
        var constructors = t.MakeGenericType(t.GetGenericArguments()).GetConstructors().ToList();

        constructors.Sort((e1, e2) => e2.GetParameters().Length.CompareTo(e1.GetParameters().Length));

        foreach (var ctor in constructors)
        {
            var parameters = ctor.GetParameters();

            var invalid = false;
            var constructedParameters = new List<object>();

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
            {
                break;
            }

            return ctor.Invoke([.. constructedParameters]);
        }
        return null;
    };
}
