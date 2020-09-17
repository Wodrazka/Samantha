using System;
using System.Collections.Generic;
using System.Text;

namespace Samantha.Binding
{
    public class FunctionBinding : IBinding
    {
        private object _lastValue;

        private IContainer _container;

        public Type ConstructionType { get; set; }

        public Scope Scope { get; set; }

        public Func<IContainer, Type, object> Function { get; set; }

        public FunctionBinding(IContainer container)
        {
            _container = container;
        }

        //TODO: Add T spezification in next c# version
        public object Get()
        {
            if(_lastValue == null || Scope == Scope.PerRequest)
            {
                _lastValue = Function(_container, ConstructionType);
            }

            return _lastValue;
        }
    }
}
