using System;
using System.Collections.Generic;
using System.Text;

namespace Samantha.Binding
{
    public class SingletonBinding : IBinding
    {
        private object _value;

        public SingletonBinding(object value)
        {
            _value = value;
        }

        public object Get()
        {
            return _value;
        }
    }
}
