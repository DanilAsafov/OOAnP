namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyCheckCollisionsCommandTest : IDisposable
{
    public RegisterIoCDependencyCheckCollisionsCommandTest()
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
    public void Execute_ValidArgument_ReturnsCheckCollisionsCommand()
    {
        var registration = new RegisterIoCDependencyCheckCollisionsCommand();
        registration.Execute();

        var cmd = Ioc.Resolve<ICommand>("Game.CheckCollisions", "obj-1");

        Assert.IsType<CheckCollisionsCommand>(cmd);
    }

    [Fact]
    public void Execute_InvalidArgumentType_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCheckCollisionsCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CheckCollisions", 123));
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCheckCollisionsCommand();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Game.CheckCollisions"));
    }
}
