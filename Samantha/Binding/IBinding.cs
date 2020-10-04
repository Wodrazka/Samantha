using System;
using System.Collections.Generic;
using System.Text;

namespace Samantha
{
    internal interface IBinding
    {
        object Get();
        object Get(Type t);
    }
}
