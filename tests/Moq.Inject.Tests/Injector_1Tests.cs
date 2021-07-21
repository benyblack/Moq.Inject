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
        public void Create_GivenMockableType_ReturnNotNull(){
            // Act
            var result1 = new Injector<ExampleClassHasConstructorWith2InterfaceInputs>().Create();
            var result2 = new Injector<ExampleClassHasConstructorWithNonMockableInputs>().Create();

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
        }
    }
}