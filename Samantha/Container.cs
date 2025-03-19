namespace Samantha;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

internal sealed class Container : IContainer
{

    private readonly ConcurrentDictionary<Type, List<IBinding>> _bindings;

    internal Container() => _bindings = new ConcurrentDictionary<Type, List<IBinding>>();

    internal void AddBinding(Type type, IBinding binding)
    {
        var typeBinding = _bindings.GetOrAdd(type, []);
        typeBinding.Add(binding);
    }

    public T Resolve<T>() => (T)Resolve(typeof(T));

    public object Resolve(Type type)
    {
        var t = type;
        if (type.IsGenericType)
        {
            t = type.GetGenericTypeDefinition();
        }

        if (_bindings.TryGetValue(t, out var bindings))
        {
            return bindings.Last().Get(type);
        }

        throw new InvalidOperationException($"Type {type} doesn't exist.");
    }
}
