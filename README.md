[![.NET](https://github.com/benyblack/Moq.Inject/actions/workflows/dotnet.yml/badge.svg)](https://github.com/benyblack/Moq.Inject/actions/workflows/dotnet.yml)
[![Nuget Package](https://img.shields.io/nuget/v/MoqInject)](https://www.nuget.org/packages/MoqInject)

# Moq.Inject
When I do unit testing (with [XUnit](https://xunit.net/) and [Moq](https://github.com/moq/moq)) in a project full of controllers or any object that requires dependency injection, I often notice that a change in the constructor will cause several changes in the unit tests. 
If the project is in an early development stage with a lot of changes coming every day, this would be even more frustrating.

This library is an easy way to solve this problem. In some cases, you may need to instantiate your class just with some default mock classes:
```csharp
[Fact]
public void Test_Something(){
    // Arrange
    var worker = Mock.Of<IWorker>();
    var configuration = Mock.Of<IConfiguration>();
    var logger = Mock.Of<ILogger>();
    var controller = new HomeController(worker, configuration, logger);
    
    // Act ...
}
```

With Moq.Inject it can be as simple as:

```csharp
[Fact]
public void Test_Something(){
    // Arrange
    var controller = Injector.Create<HomeController>();
    
    // Act ...
}

```

Let's say we need to have a custom input for ILogger:
```csharp
[Fact]
public void Test_Something(){
    // Arrange
    var logger = new Mock<ILogger>();
    logger.Setup(x=> AMethod()).Returns(something);
    var controller = new Injector<Controller>().Add("logger", logger.Object).Create();
    
    // Act ...
}

```
