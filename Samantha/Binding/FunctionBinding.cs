namespace Samantha.Binding;

using System;

public class FunctionBinding(IContainer container) : IBinding
{
    private readonly object _lock = new();

    private readonly IContainer _container = container;

    private object _lastValue;

    //TODO: init in next c# version
    public Type ConstructionType { get; set; }

    //TODO: init in next c# version
    public Scope Scope { get; set; }

    //TODO: init in next c# version
    public Func<IContainer, Type, object> Function { get; set; }

    //TODO: init in next c# version
    public bool IsGeneric { get; set; }

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
