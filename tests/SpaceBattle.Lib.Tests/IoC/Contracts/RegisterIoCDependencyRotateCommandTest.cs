namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyRotateCommandTest : IDisposable
{
    public RegisterIoCDependencyRotateCommandTest()
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
    public void Execute_RegisterRotateCommandDependency_DependencyResolved()
    {
        var rotatingAdapterMock = new Mock<IRotatable>();
        var gameEntityMock = new Mock<object>();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Adapters.IRotatable",
            (object[] args) => rotatingAdapterMock.Object
        ).Execute();

        var registerCommand = new RegisterIoCDependencyRotateCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Rotate", gameEntityMock.Object);

        Assert.IsType<RotateCommand>(resolvedCommand);
    }
}
