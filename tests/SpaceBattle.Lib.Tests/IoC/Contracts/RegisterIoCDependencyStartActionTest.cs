namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyStartActionTest : IDisposable
{
    public RegisterIoCDependencyStartActionTest()
    {
        new InitCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }

    [Fact]
    public void Execute_ValidOrder_ReturnsStartAction()
    {
        var order = new Dictionary<string, object>
        {
            {"Command", Mock.Of<ICommand>()},
            {"Receiver", Mock.Of<ICommandReceiver>()},
            {"OperationId", "Identifier"},
            {"Entity", Mock.Of<IDictionary<string, object>>()}
        };
        var registerAction = new RegisterIoCDependencyStartAction();
        registerAction.Execute();

        var resolvedAction = Ioc.Resolve<ICommand>("Actions.Start", order);

        Assert.IsType<StartAction>(resolvedAction);
    }

    [Fact]
    public void Execute_InvalidArgumentType_ThrowsArgumentException()
    {
        var registerAction = new RegisterIoCDependencyStartAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Start", "InvalidType"));
    }

    [Fact]
    public void Execute_TooManyArguments_ThrowsArgumentException()
    {
        var registerAction = new RegisterIoCDependencyStartAction();
        registerAction.Execute();
        var validArgument1 = new Dictionary<string, object>();
        var validArgument2 = new Dictionary<string, object>();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Start", validArgument1, validArgument2));
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registerAction = new RegisterIoCDependencyStartAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Start"));
    }

    [Theory]
    [InlineData("Command")]
    [InlineData("Receiver")]
    [InlineData("OperationId")]
    [InlineData("Entity")]
    public void Execute_NoKey_ThrowsArgumentException(string keyToRemove)
    {
        var order = new Dictionary<string, object>
        {
            {"Command", Mock.Of<ICommand>()},
            {"Receiver", Mock.Of<ICommandReceiver>()},
            {"OperationId", "Identifier"},
            {"Entity", Mock.Of<IDictionary<string, object>>()}
        };
        order.Remove(keyToRemove);
        var registerAction = new RegisterIoCDependencyStartAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Start", order));
    }

    [Theory]
    [InlineData("Command", null)]
    [InlineData("OperationId", null)]
    [InlineData("Receiver", null)]
    [InlineData("Entity", null)]
    public void Execute_InvalidTypeInOrder_ThrowsArgumentException(string keyToCorrupt, object badValue)
    {
        var order = new Dictionary<string, object>
        {
            {"Command", "InvalidType"},
            {"OperationId", "Identifier"},
            {"Receiver", Mock.Of<ICommandReceiver>()},
            {"Entity", Mock.Of<IDictionary<string, object>>()}
        };
        order[keyToCorrupt] = badValue;
        var registerAction = new RegisterIoCDependencyStartAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Start", order));
    }
}
