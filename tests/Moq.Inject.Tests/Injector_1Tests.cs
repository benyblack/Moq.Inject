using System;
using Xunit;

namespace Moq.Inject.Tests
{
    public class InjectorGenericsTests
    {
        [Fact]
        public void Injector_AddParam_AddedtoParameterList(){
            // Arrange & Act
            var injector = new Injector<ExampleClassHasConstructorWithNonMockableInputs>().Add("name", "Behnam").Add("age", 12);

            // Assert
            Assert.Equal(2, injector.Inputs.Count);

        }

        [Fact]
        public void Injector_AddParamDoesNotExistInConstructor_ThrowAnException(){
            // Arrange
            var injector = new Injector<ExampleClassHasConstructorWithNonMockableInputs>();
            
            // Act
            var exception = Assert.Throws<Exception>(()=> injector.Add("Name", "Behnam"));

            // Assert
            Assert.StartsWith("Parameter name was not found.", exception.Message);
        }

        [Fact]
        public void Create_GivenMockableType_ReturnNotNull(){
            // Act
            var result1 = new Injector<ExampleClassHasConstructorWithAnInterfaceInput>().Create();
            var result2 = new Injector<ExampleClassHasConstructorWith2InterfaceInputs>().Create();
            var result3 = new Injector<ExampleClassHasConstructorWithNonMockableInputs>().Create();
            

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotNull(result3);
        }

        [Fact]
        public void Create_GivenMockableTypeWithComplexConstructor_ReturnNotNull(){
            // Act
            var result = new Injector<ExampleClassHasConstructorMixInputs>()
                .Add("name", "Behnam")
                .Add("exampleInterface1", Mock.Of<IExampleInterface1>())
                .Create();
            
            // Assert
            Assert.NotNull(result);
        }
    }
}