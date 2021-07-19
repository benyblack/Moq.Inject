using System;
using System.Linq;
using System.Reflection;
using Moq;

namespace Moq.Inject
{
    public class Injector
    {

        public static object Of(Type type)
        {
            MethodInfo ofMethod = typeof(Mock).GetMethod("Of", 1, types: new Type[] { });
            MethodInfo genericOfMethod = ofMethod.MakeGenericMethod(new[] { type });
            return genericOfMethod.Invoke(null, null);
        }

        public static T Create<T>()
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];
            var types = ctor.GetParameters().Select(x => x.ParameterType).ToArray();

            // var mockedTypes = types.Select(type => 
            // There is an extension method named IsMockable is Moq
            // But it is in a internal class therefore I barrowed the method from this:
            // https://github.com/moq/moq4/blob/b7b07279c45c29a201399a7fa93764892422e687/src/Moq/Extensions.cs#L229
            bool IsMockable(Type type) => !type.IsSealed || (type.BaseType == typeof(MulticastDelegate));
            var multicastDelegateType = typeof(MulticastDelegate);
            var mockedTypes = types.Select(type =>
                IsMockable(type) ? Of(type) : GetDefaultValue(type)
            ).ToArray();
            return GetInstance<T>(mockedTypes);
        }

        private static object GetDefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
        public static T GetInstance<T>(object[] parameters)
        {
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }
    }
}
