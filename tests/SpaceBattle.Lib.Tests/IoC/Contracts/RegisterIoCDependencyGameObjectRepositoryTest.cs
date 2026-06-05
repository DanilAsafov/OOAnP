namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyGameObjectRepositoryTest : IDisposable
{
    public RegisterIoCDependencyGameObjectRepositoryTest()
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
    public void Execute_ResolveRepository_ReturnsGameObjectRepository()
    {
        var registration = new RegisterIoCDependencyGameObjectRepository();
        registration.Execute();

        var repo = Ioc.Resolve<IGameObjectRepository>("Game.Repository");

        Assert.IsType<GameObjectRepository>(repo);
    }

    [Fact]
    public void Execute_ResolveTwice_ReturnsSameInstance()
    {
        var registration = new RegisterIoCDependencyGameObjectRepository();
        registration.Execute();

        var repo1 = Ioc.Resolve<IGameObjectRepository>("Game.Repository");
        var repo2 = Ioc.Resolve<IGameObjectRepository>("Game.Repository");

        Assert.Same(repo1, repo2);
    }
}
