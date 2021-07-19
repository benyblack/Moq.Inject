using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace Moq.Inject.Tests
{
    public class InjectorTests
    {
        [Fact]
        public void Of_GivenNull_ReturnNull(){
            // Arrange
            // Act
            var result = Injector.Of(null);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(typeof(IMy))]
        [InlineData(typeof(ICollection))]
        [InlineData(typeof(IQueryable<List<string>>))]
        public void Of_GivenType_ReturnAnInstance(Type type)
        {
            // Arrange
            // Act
            var result = Injector.Of(type);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Of_GivenType_ReturnAMockedObject()
        {
            // Arrange
            var type = typeof(IMy);

            // Act
            var result = Injector.Of(type);
            var runtimeProperties = result.GetType().GetProperties();

            // Assert
            Assert.Contains(runtimeProperties,
                                x => x.Name == "Mock" && x.ReflectedType.ToString() == "Castle.Proxies.IMyProxy");
        }

        [Fact]
        public void Create_TypeHasConstructorWithoutParameters_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<Class1>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Class1>(result);
        }

        [Fact]
        public void Create_TypeHasConstructorWithAnInterfaceParameter_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<Class2>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Class2>(result);

        }

        [Fact]
        public void Create_TypeHasConstructorWithInterfaceParameters_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<Class3>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Class3>(result);

        }

        [Fact]
        public void Create_TypeHasConstructorWithNonMockableParameters_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<Class4>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Class4>(result);

        }

        [Fact]
        public void Create_TypeHasConstructorWithNonMockableParameters_InjectCorrectDefaultValues()
        {
            // Act
            var result = Injector.Create<Class4>();

            // Assert
            Assert.Equal(0, result.Age);
            Assert.Null(result.Name);

        }

        [Fact]
        public void Create_GivenOneParam_SetTheProperty(){
            // Act
            var paramDic = new Dictionary<string, object>();
            paramDic.Add("name", "Behnam");
            var result = Injector.Create<Class4>(paramDic);

            // Assert
            Assert.Equal(0, result.Age);
            Assert.Equal("Behnam", result.Name);
        }

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
}
