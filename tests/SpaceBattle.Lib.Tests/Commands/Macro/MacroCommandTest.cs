namespace SpaceBattle.Lib.Tests.Commands;

public class MacroCommandTest
{
    [Fact]
    public void Execute_ValidCommands_CompleteCommands()
    {
        var command1Mock = new Mock<ICommand>();    
        var command2Mock = new Mock<ICommand>();
        var macroCommand = new MacroCommand([command1Mock.Object, command2Mock.Object]);
        
        macroCommand.Execute();

        command1Mock.Verify(c => c.Execute(), Times.Once());
        command2Mock.Verify(c => c.Execute(), Times.Once());
    }

    [Fact]
    public void Execute_HaveInvalidCommand_CompleteNotAllCommands()
    {
        var command1Mock = new Mock<ICommand>();    
        var command2Mock = new Mock<ICommand>();
        command1Mock.Setup(c => c.Execute()).Throws<InvalidOperationException>();
        var macroCommand = new MacroCommand([command1Mock.Object, command2Mock.Object]);
        
        Assert.Throws<InvalidOperationException>(macroCommand.Execute);
        command2Mock.Verify(c => c.Execute(), Times.Never());
    }
}
