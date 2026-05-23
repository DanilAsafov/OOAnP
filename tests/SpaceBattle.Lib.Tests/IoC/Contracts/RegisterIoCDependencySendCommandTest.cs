namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencySendCommandTest : IDisposable
{
    public RegisterIoCDependencySendCommandTest()
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
    public void Execute_ValidArguments_ReturnsSendCommand()
    {
        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        var commandMock = new Mock<ICommand>();
        var receiverMock = new Mock<ICommandReceiver>();

        var resolvedCommand = Ioc.Resolve<ICommand>("Commands.Send", commandMock.Object, receiverMock.Object);

        Assert.IsType<SendCommand>(resolvedCommand);
    }

    [Theory]
    [InlineData("Invalid", "Type")]
    [InlineData(null, null)]
    public void Execute_InvalidArgumentsType_ThrowsArgumentException(object arg1, object arg2)
    {
        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Commands.Send", arg1, arg2));
    }

    [Fact]
    public void Execute_InvalidArgumentsCount_ThrowsArgumentException()
    {
        var registerCommand = new RegisterIoCDependencySendCommand();
        registerCommand.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<ICommand>("Commands.Send", Mock.Of<ICommand>()));
    }
}
