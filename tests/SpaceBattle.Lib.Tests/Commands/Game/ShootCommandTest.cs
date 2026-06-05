namespace SpaceBattle.Lib.Tests.Commands;

public class ShootCommandTest : IDisposable
{
    public ShootCommandTest()
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
    public void Execute_ValidShip_CreatesTorpedoAndStartsMovement()
    {
        // Arrange
        var shipMock = new Mock<IGameObject>();
        var torpedoMock = new Mock<IGameObject>();
        var repoMock = new Mock<IGameObjectRepository>();
        var receiverMock = new Mock<ICommandReceiver>();
        var moveCmdMock = new Mock<ICommand>();
        var injectableCmdMock = new Mock<ICommand>();
        var startCmdMock = new Mock<ICommand>();

        repoMock.Setup(r => r.Get("ship-1")).Returns(shipMock.Object);

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Repository",
            (object[] args) => repoMock.Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CreateTorpedo",
            (object[] args) => torpedoMock.Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.Move",
            (object[] args) => moveCmdMock.Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CommandReceiver",
            (object[] args) => receiverMock.Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Commands.CommandInjectable",
            (object[] args) => injectableCmdMock.Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Actions.Start",
            (object[] args) => startCmdMock.Object
        ).Execute();

        var cmd = new ShootCommand("ship-1", "torpedo-1");

        // Act
        cmd.Execute();

        // Assert
        repoMock.Verify(r => r.Get("ship-1"), Times.Once);
        repoMock.Verify(r => r.Put("torpedo-1", torpedoMock.Object), Times.Once);
        startCmdMock.Verify(c => c.Execute(), Times.Once);
    }
}
