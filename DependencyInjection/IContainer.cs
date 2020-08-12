using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public interface IContainer
    {

        T Resolve<T>();

        object Resolve(Type type);

    }
}
