namespace Samantha;

using System;
using System.Reflection;
using Samantha.Registation;

public interface IContainerBuilder
{

    IContainer Build();

    /// <summary>
    /// Register type T as Singleton
    /// 
    /// Resolve every time the same object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ISingleRegistration RegisterSingleton<T>(T singleton);

    /// <summary>
    /// Register a generic type
    /// </summary>
    /// <param name="genericType"></param>
    /// <returns></returns>
    ISingleRegistration RegisterGeneric(Type genericType);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ISingleRegistration Register<T>();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    ISingleRegistration Register<T>(Func<IContainer, Type, object> func);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    void RegisterSelf();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    IRegistrationList RegisterAssemplyTypes(Assembly assembly);


}
