namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyMacroCommandTest : IDisposable
{
    public RegisterIoCDependencyMacroCommandTest()
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
    public void Execute_WithoutArguments_DependencyResolved()
    {
        var registerCommand = new RegisterIoCDependencyMacroCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Macro");

        Assert.IsType<MacroCommand>(resolvedCommand);
    }

    [Fact]
    public void Execute_WithArguments_DependencyResolved()
    {
        var macroCommand = new ICommand[] {Mock.Of<ICommand>(), Mock.Of<ICommand>()};

        var registerCommand = new RegisterIoCDependencyMacroCommand();

        registerCommand.Execute();
        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Macro", [macroCommand]);

        Assert.IsType<MacroCommand>(resolvedCommand);
    }

    [Fact]
    public void Execute_InvalidArguments_ThrowsArgumentException()
    {
        var registerCommand = new RegisterIoCDependencyMacroCommand();

        registerCommand.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Commands.Macro", "InvalidArgument"));
    }

    [Fact]
    public void Execute_TooManyArguments_ThrowsArgumentException()
    {
        var macroCommand = new ICommand[] {Mock.Of<ICommand>(), Mock.Of<ICommand>()};
        var registerCommand = new RegisterIoCDependencyMacroCommand();

        registerCommand.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Commands.Macro", macroCommand, macroCommand));
    }
}
