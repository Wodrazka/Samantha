using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Samantha
{
    internal class Container : IContainer
    {

        private readonly ConcurrentDictionary<Type, List<IBinding>> _bindings;

        internal Container()
        {
            _bindings = new ConcurrentDictionary<Type, List<IBinding>>();
        }

        internal void AddBinding(Type type, IBinding binding)
        {
            var typeBinding = _bindings.GetOrAdd(type, new List<IBinding>());
            typeBinding.Add(binding);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (_bindings.TryGetValue(type, out List<IBinding> bindings))
            {
                return bindings.Last().Get();
            }

            throw new Exception($"Type {type} don't exists");
        }
    }
}
