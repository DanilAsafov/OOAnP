namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyMoveCommandTest : IDisposable
{
    public RegisterIoCDependencyMoveCommandTest()
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
    public void Execute_RegisterMoveCommandDependency_DependencyResolved()
    {
        var movingAdapterMock = new Mock<IMovable>();
        var gameEntityMock = new Mock<object>();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Adapters.IMovable",
            (object[] args) => movingAdapterMock.Object
        ).Execute();

        var registerCommand = new RegisterIoCDependencyMoveCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Move", gameEntityMock.Object);

        Assert.IsType<MoveCommand>(resolvedCommand);
    }
}
