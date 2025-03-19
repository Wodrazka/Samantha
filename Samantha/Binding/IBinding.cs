namespace Samantha;

using System;

internal interface IBinding
{
    public object Get();
    public object Get(Type t);
}
