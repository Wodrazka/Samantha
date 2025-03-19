namespace Samantha;

using System;

public interface IContainer
{

    public T Resolve<T>();

    public object Resolve(Type type);

}
