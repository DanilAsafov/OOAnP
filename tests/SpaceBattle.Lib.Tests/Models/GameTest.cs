namespace SpaceBattle.Lib.Tests.Models;

public class GameTest : IDisposable
{
    public GameTest()
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
    public void Execute_ValidOrder_ChecksAuthAndExecutesAction()
    {
        // Arrange
        var checkAuthMock = new Mock<ICommand>();
        var actionMock = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CheckAuth",
            (object[] args) => checkAuthMock.Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Actions.Shoot",
            (object[] args) => actionMock.Object
        ).Execute();

        var order = new Dictionary<string, object>
        {
            { "PlayerId", "player-1" },
            { "GameObjectId", "ship-1" },
            { "Action", "Shoot" }
        };

        var game = new Game(order);

        // Act
        game.Execute();

        // Assert
        checkAuthMock.Verify(c => c.Execute(), Times.Once);
        actionMock.Verify(a => a.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_AuthFails_ThrowsAndDoesNotExecuteAction()
    {
        // Arrange
        var checkAuthMock = new Mock<ICommand>();
        checkAuthMock.Setup(c => c.Execute()).Throws(new UnauthorizedAccessException("Нет доступа"));

        var actionMock = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CheckAuth",
            (object[] args) => checkAuthMock.Object
        ).Execute();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Actions.Shoot",
            (object[] args) => actionMock.Object
        ).Execute();

        var order = new Dictionary<string, object>
        {
            { "PlayerId", "player-1" },
            { "GameObjectId", "ship-1" },
            { "Action", "Shoot" }
        };

        var game = new Game(order);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => game.Execute());
        actionMock.Verify(a => a.Execute(), Times.Never);
    }

    [Fact]
    public void Constructor_NullOrder_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Game(null!));
    }

    [Theory]
    [InlineData("PlayerId")]
    [InlineData("GameObjectId")]
    [InlineData("Action")]
    public void Execute_MissingKey_ThrowsKeyNotFoundException(string keyToRemove)
    {
        var order = new Dictionary<string, object>
        {
            { "PlayerId", "player-1" },
            { "GameObjectId", "ship-1" },
            { "Action", "Shoot" }
        };
        order.Remove(keyToRemove);

        var game = new Game(order);

        Assert.Throws<KeyNotFoundException>(() => game.Execute());
    }
}
