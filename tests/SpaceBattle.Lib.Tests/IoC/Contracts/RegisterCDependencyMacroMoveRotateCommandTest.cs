namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyMacroMoveRotateTest : IDisposable
{
    public RegisterIoCDependencyMacroMoveRotateTest()
    {
        new InitCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
    }

    [Theory]
    [InlineData("Move")]
    [InlineData("Rotate")]
    public void Execute_RegisterMacroMoveRotateDependency_DependencyResolved(string command)
    {
        var gameEntityMock = new Mock<object>();

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            $"Specs.{command}",
            (object[] args) => new[] { $"{command}.Command1" }
        ).Execute();

        var innerCommandMock = new Mock<ICommand>();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            $"{command}.Command1",
            (object[] args) => innerCommandMock.Object
        ).Execute();

        var registerCommand = new RegisterIoCDependencyMacroMoveRotateCommand();
        registerCommand.Execute();

        var resolvedCommand = Ioc.Resolve<ICommand>($"Macro.{command}", gameEntityMock.Object);

        Assert.IsType<MacroCommand>(resolvedCommand);
    }
}
