using System;

namespace Moq.Inject.Tests
{
    public interface IExampleInterface1
    {
        string A();
        int B();
        int Age { get; set; }
    }

    public interface IExampleInterface2
    {
        DateTime C();
        long D();
        string Name { get; set; }
    }

    public class ExampleClassWithDefaultConstructor
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class ExampleClassHasConstructorWithAnInterfaceInput
    {

        private readonly IExampleInterface1 _exampleInterface1;
        public ExampleClassHasConstructorWithAnInterfaceInput(IExampleInterface1 exampleInterface1)
        {
            _exampleInterface1 = exampleInterface1;
        }

    }

    public class ExampleClassHasConstructorWith2InterfaceInputs
    {

        private readonly IExampleInterface1 _exampleInterface1;
        private readonly IExampleInterface2 _exampleInterface2;
        public ExampleClassHasConstructorWith2InterfaceInputs(IExampleInterface1 exampleInterface1, IExampleInterface2 exampleInterface2)
        {
            _exampleInterface1 = exampleInterface1;
            _exampleInterface2 = exampleInterface2;
        }

    }

    public class ExampleClassHasConstructorWithNonMockableInputs
    {

        public int Age;
        public string Name;
        public string[] Siblings;
        public ExampleClassHasConstructorWithNonMockableInputs(int age, string name, string[] siblings)
        {
            Age = age;
            Name = name;
            Siblings = siblings;
        }

    }

    public class ExampleClassHasConstructorMixInputs
    {

        public int Age;
        public string Name;
        public string[] Siblings;
        private readonly IExampleInterface1 _exampleInterface1;
        public ExampleClassHasConstructorMixInputs(int age, string name, string[] siblings, IExampleInterface1 exampleInterface1)
        {
            Age = age;
            Name = name;
            Siblings = siblings;
            _exampleInterface1 = exampleInterface1;
        }

    }
}