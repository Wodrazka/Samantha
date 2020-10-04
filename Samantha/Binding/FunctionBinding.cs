using System;

namespace Samantha.Binding
{
    public class FunctionBinding : IBinding
    {
        private readonly object _lock;

        private readonly IContainer _container;

        private object _lastValue;

        //TODO: init in next c# version
        public Type ConstructionType { get; set; }

        //TODO: init in next c# version
        public Scope Scope { get; set; }

        //TODO: init in next c# version
        public Func<IContainer, Type, object> Function { get; set; }

        //TODO: init in next c# version
        public bool IsGeneric { get; set; } = false;

        public FunctionBinding(IContainer container)
        {
            _container = container;
            _lock = new object();
        }

        //TODO: Add T spezification in next c# version
        public object Get()
        {
            lock (_lock)
            {

                if (_lastValue == null || Scope == Scope.PerRequest)
                {
                        _lastValue = Function(_container, ConstructionType);
                }

                return _lastValue;
            }
        }

        //TODO: Add T spezification in next c# version
        public object Get(Type t)
        {
            lock (_lock)
            {

                if (_lastValue == null || Scope == Scope.PerRequest)
                {
                    if (IsGeneric && t.IsGenericType)
                    {
                        _lastValue = Function(_container, ConstructionType.MakeGenericType(t.GetGenericArguments()));
                    }
                    else
                    {
                        _lastValue = Function(_container, ConstructionType);
                    }
                }

                return _lastValue;
            }
        }
    }
}
