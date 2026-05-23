namespace SpaceBattle.Lib.Tests.Commands;

public class CommandInjectableCommandTest
{
    [Fact]
    public void Execute_CommandInjected_ExecutesInjectedCommand()
    {
        var injectedCommandMock = new Mock<ICommand>();
        var injectableCommand = new CommandInjectableCommand();
        injectableCommand.Inject(injectedCommandMock.Object);

        injectableCommand.Execute();

        injectedCommandMock.Verify(c => c.Execute(), Times.Once());
    }

    [Fact]
    public void Execute_CommandNotInjected_ThrowsInvalidOperationException()
    {
        var injectableCommand = new CommandInjectableCommand();

        Assert.Throws<InvalidOperationException>(injectableCommand.Execute);
    }
}
