using System.Collections.Generic;
using Xunit;

namespace Moq.Inject.Tests
{
    public class InjectorTests
    {

        [Fact]
        public void Create_TypeHasConstructorWithoutParameters_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<ExampleClassWithDefaultConstructor>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExampleClassWithDefaultConstructor>(result);
        }

        [Fact]
        public void Create_TypeHasConstructorWithAnInterfaceParameter_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<ExampleClassHasConstructorWithAnInterfaceInput>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExampleClassHasConstructorWithAnInterfaceInput>(result);

        }

        [Fact]
        public void Create_TypeHasConstructorWithInterfaceParameters_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<ExampleClassHasConstructorWith2InterfaceInputs>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExampleClassHasConstructorWith2InterfaceInputs>(result);

        }

        [Fact]
        public void Create_TypeHasConstructorWithNonMockableParameters_ReturnAnInsatnceSameType()
        {
            // Act
            var result = Injector.Create<ExampleClassHasConstructorWithNonMockableInputs>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExampleClassHasConstructorWithNonMockableInputs>(result);

        }

        [Fact]
        public void Create_TypeHasConstructorWithNonMockableParameters_InjectCorrectDefaultValues()
        {
            // Act
            var result = Injector.Create<ExampleClassHasConstructorWithNonMockableInputs>();

            // Assert
            Assert.Equal(0, result.Age);
            Assert.Null(result.Name);

        }

        [Fact]
        public void Create_GivenOneParam_SetTheProperty(){
            // Act
            Dictionary<string, object> paramDic = new () { { "name", "Behnam" } };
            var result = Injector.Create<ExampleClassHasConstructorWithNonMockableInputs>(paramDic);

            // Assert
            Assert.Equal(0, result.Age);
            Assert.Equal("Behnam", result.Name);
        }
    }
}
