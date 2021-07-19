using System;
using System.Collections.Generic;

namespace Moq.Inject
{
    public class Injector<T>
    {
        public Dictionary<string, object> Inputs {get; private set;}

        public Injector()
        {
            Inputs = new Dictionary<string, object>();
        }

        public Injector<T> Add(string name, Object value)
        {
            Inputs.Add(name, value);
            return this;
        }

        public T Create()
        {
            return Injector.Create<T>(Inputs);
        }
    }
}