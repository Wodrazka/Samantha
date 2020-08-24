using Samantha.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samantha
{
    public class Container : IContainer
    {

        private Dictionary<Type, List<IBinding>> _bindings;

        public Container()
        {
            _bindings = new Dictionary<Type, List<IBinding>>();
        }

        internal void AddBinding(Type type, IBinding binding)
        {
            if (binding is DynamicBinding db)
                db.GetBinding = t =>
                {
                    if (!_bindings.ContainsKey(t))
                        throw new Exception("Type don't exists");

                    return _bindings[t].Last().Get();
                };

            if (_bindings.ContainsKey(type))
                _bindings[type].Add(binding);
            else
                _bindings.Add(type, new List<IBinding>() { binding });
        }

        public T Resolve<T>()
        {
            if (!_bindings.ContainsKey(typeof(T)))
                throw new Exception("Type don't exists");

            return (T)_bindings[typeof(T)].Last().Get();
        }

        public object Resolve(Type type)
        {
            if (!_bindings.ContainsKey(type))
                throw new Exception("Type don't exists");

            return _bindings[type].Last().Get();
        }
    }
}
