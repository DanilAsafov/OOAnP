namespace SpaceBattle.Lib.Tests.Strategies;

public class CreateMacroCommandStrategyTests : IDisposable
{
    private const string MacroName = "Macro.Test";
    private const string SpecKey = $"Specs.{MacroName}";

    public CreateMacroCommandStrategyTests()
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
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void Constructor_InvalidCommandSpec_ThrowsArgumentException(string invalidSpec)
    {
        var exception = Assert.Throws<ArgumentException>(() => new CreateMacroCommandStrategy(invalidSpec));
        Assert.Equal("commandSpec", exception.ParamName);
        Assert.Contains("Спецификация команды не может быть пустой или null.", exception.Message);
    }

    [Fact]

    public void Resolve_SpecIsNull_ThrowsInvalidOperationException() {
        Ioc.Resolve<App.ICommand>("IoC.Register", SpecKey,
            (object[] args) => (IEnumerable<string>)null!
        ).Execute();

        var strategy = new CreateMacroCommandStrategy(MacroName);
        var ex = Assert.Throws<InvalidOperationException>(() => strategy.Resolve([]));
        Assert.Contains($"Спецификация команды '{SpecKey}' разрешилась в null.", ex.Message);
    }

    [Fact]
    public void Resolve_ArgsAreNull_ThrowsArgumentNullException()
    {
        var strategy = new CreateMacroCommandStrategy(MacroName);
        Assert.Throws<ArgumentNullException>(() => strategy.Resolve(null!));
    }

    [Fact]
    public void Resolve_SpecIsEmpty_ThrowsInvalidOperationException()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", SpecKey, 
            (object[] args) => Array.Empty<string>()
        ).Execute();

        var strategy = new CreateMacroCommandStrategy(MacroName);

        var ex = Assert.Throws<InvalidOperationException>(() => strategy.Resolve([]));
        Assert.Contains($"Спецификация команды '{SpecKey}' не содержит команд.", ex.Message);
    }

    [Fact]
    public void Resolve_ValidMacroSpec_ExecutesAllCommandsInOrder()
    {
        var commandNames = new[] { "Command.Test1", "Command.Test2", "Command.Test3" };
        var executionOrder = new List<string>();

        Ioc.Resolve<App.ICommand>("IoC.Register", SpecKey, 
            (object[] args) => commandNames
        ).Execute();

        foreach (var name in commandNames)
        {
            var localName = name;
            var mock = new Mock<ICommand>();
            mock.Setup(c => c.Execute()).Callback(() => executionOrder.Add(localName));
            
            Ioc.Resolve<App.ICommand>("IoC.Register", localName, 
                (object[] args) => mock.Object
            ).Execute();
        }

        Ioc.Resolve<App.ICommand>("IoC.Register", MacroName, 
            (object[] args) => new CreateMacroCommandStrategy(MacroName).Resolve(args)
        ).Execute();

        var macroCommand = Ioc.Resolve<ICommand>(MacroName);
        macroCommand.Execute();

        Assert.True(commandNames.SequenceEqual(executionOrder));
    }

    [Fact]
    public void Resolve_MacroSpecNotRegistered_ThrowsException()
    {
        var missingMacroName = "Macro.NonExistent";
        Ioc.Resolve<App.ICommand>("IoC.Register", missingMacroName,
            (object[] args) => new CreateMacroCommandStrategy(missingMacroName).Resolve(args)
        ).Execute();

        Assert.Throws<InvalidOperationException>(() => Ioc.Resolve<ICommand>(missingMacroName));
    }

    [Fact]
    public void Resolve_CommandInSpecNotRegistered_ThrowsException()
    {
        Ioc.Resolve<App.ICommand>("IoC.Register", SpecKey,
            (object[] args) => new[] { "Command.Exists", "Command.NotExists" }
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Command.Exists",
            (object[] args) => Mock.Of<ICommand>()
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", MacroName,
            (object[] args) => new CreateMacroCommandStrategy(MacroName).Resolve(args)
        ).Execute();
        
        var ex = Assert.Throws<InvalidOperationException>(() => Ioc.Resolve<ICommand>(MacroName));

        Assert.Contains("Не удалось разрешить команду 'Command.NotExists'", ex.Message);
    }
}
