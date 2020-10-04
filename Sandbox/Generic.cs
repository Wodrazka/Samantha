using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public class Generic<T>
    {
        public Type Type { get; set; }

        public Generic()
        {
            Type = typeof(T);
        }

    }
}
