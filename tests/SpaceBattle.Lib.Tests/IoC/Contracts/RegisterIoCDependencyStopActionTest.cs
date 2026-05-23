namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyStopActionTest : IDisposable
{
    public RegisterIoCDependencyStopActionTest()
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
    public void Execute_ValidOrder_ReturnsStopAction()
    {
        var order = new Dictionary<string, object>
        {
            {"OperationId", "Identifier"},
            {"Entity", Mock.Of<IDictionary<string, object>>()}
        };
        var registerAction = new RegisterIoCDependencyStopAction();
        registerAction.Execute();

        var resolvedAction = Ioc.Resolve<ICommand>("Actions.Stop", order);

        Assert.IsType<StopAction>(resolvedAction);
    }

    [Fact]
    public void Execute_InvalidArgumentType_ThrowsArgumentException()
    {
        var registerAction = new RegisterIoCDependencyStopAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Stop", "InvalidType"));
    }

    [Fact]
    public void Execute_TooManyArguments_ThrowsArgumentException()
    {
        var registerAction = new RegisterIoCDependencyStopAction();
        registerAction.Execute();
        var validArgument1 = new Dictionary<string, object>();
        var validArgument2 = new Dictionary<string, object>();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Stop", validArgument1, validArgument2));
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registerAction = new RegisterIoCDependencyStopAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Stop"));
    }

    [Theory]
    [InlineData("OperationId")]
    [InlineData("Entity")]
    public void Execute_NoKey_ThrowsArgumentException(string keyToRemove)
    {
        var order = new Dictionary<string, object>
        {
            {"OperationId", "Identifier"},
            {"Entity", Mock.Of<IDictionary<string, object>>()}
        };
        order.Remove(keyToRemove);
        var registerAction = new RegisterIoCDependencyStopAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Stop", order));
    }

    [Theory]
    [InlineData("OperationId", null)]
    [InlineData("Entity", null)]
    public void Execute_InvalidTypeInOrder_ThrowsArgumentException(string keyToCorrupt, object badValue)
    {
        var order = new Dictionary<string, object>
        {
            {"OperationId", "Identifier"},
            {"Entity", Mock.Of<IDictionary<string, object>>()}
        };
        order[keyToCorrupt] = badValue;
        var registerAction = new RegisterIoCDependencyStopAction();
        registerAction.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Actions.Stop", order));
    }
}
