namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyShootCommandTest : IDisposable
{
    public RegisterIoCDependencyShootCommandTest()
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
    public void Execute_ValidOrder_ReturnsShootCommand()
    {
        var order = new Dictionary<string, object>
        {
            { "GameObjectId", "ship-1" }
        };
        var registration = new RegisterIoCDependencyShootCommand();
        registration.Execute();

        var resolved = Ioc.Resolve<ICommand>("Game.Actions.Shoot", order);

        Assert.IsType<ShootCommand>(resolved);
    }

    [Fact]
    public void Execute_InvalidArgumentType_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyShootCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.Actions.Shoot", "InvalidType"));
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyShootCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.Actions.Shoot"));
    }

    [Fact]
    public void Execute_MissingGameObjectId_ThrowsArgumentException()
    {
        var order = new Dictionary<string, object>();
        var registration = new RegisterIoCDependencyShootCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.Actions.Shoot", order));
    }
}
