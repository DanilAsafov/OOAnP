namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyCollisionDetectCommandTest : IDisposable
{
    public RegisterIoCDependencyCollisionDetectCommandTest()
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
    public void Execute_ValidArguments_ReturnsCollisionDetectCommand()
    {
        var registration = new RegisterIoCDependencyCollisionDetectCommand();
        registration.Execute();

        var cmd = Ioc.Resolve<ICommand>("Game.CollisionDetect", "obj-1", "obj-2");

        Assert.IsType<CollisionDetectCommand>(cmd);
    }

    [Fact]
    public void Execute_InvalidArgumentsType_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCollisionDetectCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CollisionDetect", 123, 456));
    }

    [Fact]
    public void Execute_OneArgument_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCollisionDetectCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CollisionDetect", "obj-1"));
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCollisionDetectCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CollisionDetect"));
    }
}
