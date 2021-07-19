using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moq.Inject
{
    public class Injector
    {

        public static object Of(Type type)
        {
            if (type == null) return null;
            if (!IsMockable(type)) return null;

            MethodInfo ofMethod = typeof(Mock).GetMethod("Of", 1, types: new Type[] { });
            MethodInfo genericOfMethod = ofMethod.MakeGenericMethod(new[] { type });
            return genericOfMethod.Invoke(null, null);
        }

        public static T Create<T>(Dictionary<string, object> inputs)
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];
            var ctorParams = ctor.GetParameters();
            var types = ctorParams.Select(x => x.ParameterType).ToArray();
            var names = ctorParams.Select(x => x.ParameterType).ToArray();

            SortedDictionary<int, object> parameters = new SortedDictionary<int, object>();
            foreach (var param in ctorParams)
            {
                if (inputs.Any(x => x.Key == param.Name))
                {
                    parameters.Add(param.Position, inputs.First(x => x.Key == param.Name).Value);
                }
                else
                {
                    parameters.Add(param.Position, Of(param.ParameterType));
                }
            }
            return GetInstance<T>(parameters.Values.ToArray());
        }

        public static T Create<T>()
        {
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];
            var types = ctor.GetParameters().Select(x => x.ParameterType).ToArray();
            var mockedTypes = types.Select(type => IsMockable(type) ? Of(type) : GetDefaultValue(type)).ToArray();
            return GetInstance<T>(mockedTypes);
        }

        private static object GetDefaultValue(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;

        // var mockedTypes = types.Select(type => 
        // There is an extension method named IsMockable is Moq
        // But it is in a internal class therefore I barrowed the method from this:
        // https://github.com/moq/moq4/blob/b7b07279c45c29a201399a7fa93764892422e687/src/Moq/Extensions.cs#L229
        private static bool IsMockable(Type type) => !type.IsSealed || (type.BaseType == typeof(MulticastDelegate));

        public static T GetInstance<T>(object[] parameters)
        {
            return (T)Activator.CreateInstance(typeof(T), parameters);
        }

    }
}
