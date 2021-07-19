using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Moq.Inject.Tests
{
    public class InjectorGenericsTests
    {
        [Fact]
        public void Injector_AddParam_AddedtoParameterList(){
            // Arrange & Act
            var injector = new Injector<Class4>().Add("name", "Behnam").Add("age", 12);

            // Assert
            Assert.Equal(2, injector.Inputs.Count);

        }

        [Fact]
        public void Create_GivenMockableType_ReturnNotNull(){
            // Act
            var result1 = new Injector<Class3>().Create();
            var result2 = new Injector<Class4>().Create();

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
        }
    }
}