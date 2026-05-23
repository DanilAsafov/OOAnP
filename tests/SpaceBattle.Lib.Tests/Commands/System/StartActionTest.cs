namespace SpaceBattle.Lib.Tests.Commands;

public class StartActionTest : IDisposable
{
    public StartActionTest()
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
    public void Execute_ValidOrder_SavesCommandAndCallsReceiver()
    {
        var commandMock = new Mock<ICommand>();
        var receiverMock = new Mock<ICommandReceiver>();
        var gameEntityMock = new Mock<IDictionary<string, object>>();
        var operationId = "Identifier";
        var order = new Dictionary<string, object>
        {
            {"Command", commandMock.Object},
            {"Receiver", receiverMock.Object},
            {"OperationId", operationId},
            {"Entity", gameEntityMock.Object}
        };
        var registerAction = new RegisterIoCDependencyStartAction();
        registerAction.Execute();
        var resolvedAction = Ioc.Resolve<ICommand>("Actions.Start", order);

        resolvedAction.Execute();

        receiverMock.Verify(r => r.Receive(commandMock.Object));
        gameEntityMock.VerifySet(g => g[operationId] = commandMock.Object);
    }
}
