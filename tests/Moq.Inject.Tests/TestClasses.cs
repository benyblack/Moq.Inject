using System;

namespace Moq.Inject.Tests
{
    public interface IMy
    {
        string A();
        int B();
        int Age { get; set; }
    }

    public interface IAm
    {
        DateTime C();
        long D();
        string Name { get; set; }
    }

    public class Class1
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public class Class2
    {

        private readonly IMy _my;
        public Class2(IMy my)
        {
            _my = my;
        }

    }

    public class Class3
    {

        private readonly IMy _my;
        private readonly IAm _am;
        public Class3(IMy my, IAm am)
        {
            _my = my;
            _am = am;
        }

    }

    public class Class4
    {

        public int Age;
        public string Name;
        public Class4(int age, string name)
        {
            Age = age;
            Name = name;
        }

    }
}