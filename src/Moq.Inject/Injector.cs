using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moq.Inject
{
    public class Injector
    {
        /// <summary>
        /// Basically calls Moq.Mock&lt;T&gt;() after some checks on the input.
        /// If the given type is not mockable, it returns null.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Mocked object by Moq</returns>
        private static object Of(Type type)
        {
            if (type == null) return null;
            if (!IsMockable(type)) return null;

            MethodInfo ofMethod = typeof(Mock).GetMethod("Of", 1, types: new Type[] { });
            MethodInfo genericOfMethod = ofMethod?.MakeGenericMethod(type);
            return genericOfMethod?.Invoke(null, null);
        }

        /// <summary>
        /// Create an instance of Given type. It uses inputs to fill constructor paramets.
        /// Mocked objects are used for missing parameters.
        /// If a parameter is not mockable it uses default value of that type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputs">To be used as constructor parameters</param>
        /// <returns>An instance of the given type</returns>
        public static T Create<T>(Dictionary<string, object> inputs)
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];
            var ctorParams = ctor.GetParameters();

            SortedDictionary<int, object> parameters = new SortedDictionary<int, object>();
            foreach (var param in ctorParams)
            {
                var value = inputs.Any(x => x.Key == param.Name) ? inputs.First(x => x.Key == param.Name).Value : Of(param.ParameterType);
                parameters.Add(param.Position, value);

            }
            return GetInstance<T>(parameters.Values.ToArray());
        }

        /// <summary>
        /// Create an instance of Given type. It also fills constructor parameters by mocked object wherever it is possible.
        /// If a parameter is not mockable it uses default value of that type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>An instance of the given type</returns>
        public static T Create<T>()
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];
            var types = ctor.GetParameters().Select(x => x.ParameterType).ToArray();
            var mockedTypes = types.Select(type => IsMockable(type) ? Of(type) : GetDefaultValue(type)).ToArray();
            return GetInstance<T>(mockedTypes);
        }

        /// <summary>
        /// Get default value of the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>It returns default value for value types. If the given type is a reference type it returns null.</returns>
        private static object GetDefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;

        /// <summary>
        /// There is an extension method named IsMockable in Moq source. 
        /// But it is in a internal class therefore I barrowed the method from this:
        /// https://github.com/moq/moq4/blob/b7b07279c45c29a201399a7fa93764892422e687/src/Moq/Extensions.cs#L229
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsMockable(Type type) => !type.IsSealed || (type.BaseType == typeof(MulticastDelegate));


        /// <summary>
        /// Create an instance of an object
        /// </summary>
        /// <param name="parameters">To be used as constructor parameters</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T GetInstance<T>(object[] parameters)
        {
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }

    }
}
