using System;

namespace Samantha
{
    internal interface IBinding
    {
        object Get();
        object Get(Type t);
    }
}
