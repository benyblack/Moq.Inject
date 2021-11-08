using System;
using System.Collections.Generic;
using System.Linq;

namespace Moq.Inject
{
    public class Injector<T>
    {
        public Dictionary<string, object> Inputs { get; }
        private readonly string[] _parametNames;

        public Injector()
        {
            Inputs = new Dictionary<string, object>();
            var ctors = typeof(T).GetConstructors();
            var ctor = ctors[0];
            _parametNames = ctor.GetParameters().Select(x => x.Name).ToArray();
        }

        /// <summary>
        /// Adds a parameter to the constructor of the object to be instantiated
        /// </summary>
        /// <param name="name">Name of the parameter in the constructor. This is case sensitive.</param>
        /// <param name="value">The value to be used for the parameter</param>
        /// <returns></returns>
        public Injector<T> Add(string name, Object value)
        {
            if (!_parametNames.Contains(name))
            {
                string errorMessage = "Parameter name was not found.";
                if (_parametNames is { Length: > 0 })
                {
                    errorMessage = $"{errorMessage} It can be one of({string.Join(", ", _parametNames)})";
                }
                throw new Exception(errorMessage);
            }
            Inputs.Add(name, value);
            return this;
        }

        /// <summary>
        /// Create an instance of an object with the given constructor parameters.
        /// It uses a mocked object for a mockable or a default value for the others if a parameter is not provided.
        /// </summary>
        /// <returns></returns>
        public T Create()
        {
            return Injector.Create<T>(Inputs);
        }
    }
}