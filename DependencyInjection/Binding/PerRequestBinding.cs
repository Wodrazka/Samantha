using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Binding
{
    public class PerRequestBinding : IBinding
    {

        public PerRequestBinding()
        {

        }

        public object Get()
        {
            throw new NotImplementedException();
        }
    }
}
