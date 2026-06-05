namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyGameAreaTest : IDisposable
{
    public RegisterIoCDependencyGameAreaTest()
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
    public void Execute_ResolveArea_ReturnsGameArea()
    {
        var registration = new RegisterIoCDependencyGameArea(10);
        registration.Execute();

        var area = Ioc.Resolve<GameArea>("Game.Area");

        Assert.IsType<GameArea>(area);
    }

    [Fact]
    public void Execute_ResolveTwice_ReturnsSameInstance()
    {
        var registration = new RegisterIoCDependencyGameArea(10);
        registration.Execute();

        var area1 = Ioc.Resolve<GameArea>("Game.Area");
        var area2 = Ioc.Resolve<GameArea>("Game.Area");

        Assert.Same(area1, area2);
    }
}
