using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samantha
{
    public class SingletonRegistration : Registration
    {

        public object Value { get; set; }

        public SingletonRegistration()
        {
            
        }
    }
}
