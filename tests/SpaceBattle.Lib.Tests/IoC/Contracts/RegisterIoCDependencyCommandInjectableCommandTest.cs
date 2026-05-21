namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyCommandInjectableCommandTest : IDisposable
{
    public RegisterIoCDependencyCommandInjectableCommandTest()
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
    public void Execute_ResolvesICommandWithoutArgument_ReturnsCommandInjectableCommand()
    {
        var registerCommand = new RegisterIoCDependencyCommandInjectableCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.CommandInjectable");

        Assert.IsType<CommandInjectableCommand>(resolvedCommand);
    }

    [Fact]
    public void Execute_ResolvesICommandInjectableWithoutArgument_ReturnsCommandInjectableCommand()
    {
        var registerCommand = new RegisterIoCDependencyCommandInjectableCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<ICommandInjectable>("Commands.CommandInjectable");

        Assert.IsAssignableFrom<ICommandInjectable>(resolvedCommand);
    }

    [Fact]
    public void Execute_ResolvesCommandInjectableCommandWithoutArgument_ReturnsCommandInjectableCommand()
    {
        var registerCommand = new RegisterIoCDependencyCommandInjectableCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<CommandInjectableCommand>("Commands.CommandInjectable");

        Assert.IsType<CommandInjectableCommand>(resolvedCommand);
    }

    [Fact]
    public void Execute_WithArgument_DependencyResolved()
    {
        var commandMock = new Mock<ICommand>();
        var registerCommand = new RegisterIoCDependencyCommandInjectableCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.CommandInjectable", commandMock.Object);

        Assert.IsType<CommandInjectableCommand>(resolvedCommand);
    }

    [Fact]
    public void Execute_InvalidArgument_ThrowsArgumentException()
    {
        var registerCommand = new RegisterIoCDependencyCommandInjectableCommand();

        registerCommand.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Commands.CommandInjectable", "InvalidArgument"));
    }

    [Fact]
    public void Execute_MoreThanOneArgument_ThrowsArgumentException()
    {
        var registerCommand = new RegisterIoCDependencyCommandInjectableCommand();

        registerCommand.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Commands.CommandInjectable", Mock.Of<ICommand>(), Mock.Of<ICommand>()));
    }
}
