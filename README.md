# Moq.Inject
When I do unit testing (with the help of Moq) on a project full of controllers or any object that needs dependency injection,
it happens a lot that changes in the constructor leads to several changes in the unit tests. 
It would me more annoying when the project is in the early development stage with a lot of changes comming every day.

This library is an easyway to manage this problem. You may need to instanciate your class just with default mock classes:
```csharp
[Fact]
public void Test_Something(){
    // Arrange
    var worker = Mock.Of<IWorker>();
    var configuration = Mock.Of<IConfiguration>();
    var logger = Mock.Of<ILogger>();
    var controller = new HomeController(worker, configuration, logger);
    
    // Act

    // Assert ...
}
```

With Moq.Inject it can be as simple as:

```csharp
[Fact]
public void Test_Something(){
    // Arrange
    var controller = Injector.Create<HomeController>();
    
    // Act

    // Assert ...
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
    
    // Act

    // Assert ...
}

```