namespace SpaceBattle.Lib.Tests.Commands;

public class StopActionTest : IDisposable
{
    public StopActionTest()
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
    public void Execute_ValidOrder_RemovesCommand()
    {
        var operationId = "Identifier";
        var gameEntity = new Dictionary<string, object> {{operationId, Mock.Of<ICommand>()}};

        new StopAction(operationId, gameEntity).Execute();

        Assert.Empty(gameEntity);
    }
}
