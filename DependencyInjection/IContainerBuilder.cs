using Samantha.Registation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Samantha
{
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ISingleRegistration Register<T>();

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
        ICollectionRegistration RegisterAssemplyTypes(Assembly assembly);


    }
}
