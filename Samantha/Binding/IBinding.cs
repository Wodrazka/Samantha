namespace Samantha;

using System;

internal interface IBinding
{
    object Get();
    object Get(Type t);
}
