using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Samantha
{
    internal class Container : IContainer
    {

        private readonly ConcurrentDictionary<Type, ConcurrentBag<IBinding>> _bindings;

        internal Container()
        {
            _bindings = new ConcurrentDictionary<Type, ConcurrentBag<IBinding>>();
        }

        internal void AddBinding(Type type, IBinding binding)
        {
            var typeBinding = _bindings.GetOrAdd(type, new ConcurrentBag<IBinding>());
            typeBinding.Add(binding);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            if (_bindings.TryGetValue(type, out ConcurrentBag<IBinding> bindings))
            {
                return bindings.Last().Get();
            }

            throw new Exception($"Type {type} don't exists");
        }
    }
}
