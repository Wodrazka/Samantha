namespace Samantha;

using System;
using System.Reflection;
using Samantha.Registation;

public interface IContainerBuilder
{

    public IContainer Build();

    /// <summary>
    /// Register type T as Singleton
    /// 
    /// Resolve every time the same object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ISingleRegistration RegisterSingleton<T>(T singleton);

    /// <summary>
    /// Register a generic type
    /// </summary>
    /// <param name="genericType"></param>
    /// <returns></returns>
    public ISingleRegistration RegisterGeneric(Type genericType);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ISingleRegistration Register<T>();

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public ISingleRegistration Register<T>(Func<IContainer, Type, object> func);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void RegisterSelf();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public IRegistrationList RegisterAssemplyTypes(Assembly assembly);


}
