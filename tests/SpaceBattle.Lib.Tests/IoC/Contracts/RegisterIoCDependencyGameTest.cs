namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyGameTest : IDisposable
{
    public RegisterIoCDependencyGameTest()
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
    public void Execute_ValidOrder_ReturnsGame()
    {
        var order = new Dictionary<string, object>
        {
            { "PlayerId", "player-1" },
            { "GameObjectId", "ship-1" },
            { "Action", "Shoot" }
        };
        var registration = new RegisterIoCDependencyGame();
        registration.Execute();

        var resolved = Ioc.Resolve<ICommand>("Game.Command", order);

        Assert.IsType<Game>(resolved);
    }

    [Fact]
    public void Execute_InvalidArgumentType_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyGame();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.Command", "InvalidType"));
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyGame();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.Command"));
    }
}
