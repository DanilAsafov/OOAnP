namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyCheckAuthCommandTest : IDisposable
{
    public RegisterIoCDependencyCheckAuthCommandTest()
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
    public void Execute_ValidArgs_ReturnsCheckAuthCommand()
    {
        var registration = new RegisterIoCDependencyCheckAuthCommand();
        registration.Execute();

        var resolved = Ioc.Resolve<ICommand>("Game.CheckAuth", "player-1", "ship-1");

        Assert.IsType<CheckAuthCommand>(resolved);
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCheckAuthCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CheckAuth"));
    }

    [Fact]
    public void Execute_OneArgument_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCheckAuthCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CheckAuth", "player-1"));
    }

    [Fact]
    public void Execute_NonStringArguments_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCheckAuthCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CheckAuth", 123, 456));
    }
}
