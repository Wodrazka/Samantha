using System;

namespace Samantha
{
    public interface IContainer
    {

        T Resolve<T>();

        object Resolve(Type type);

    }
}
