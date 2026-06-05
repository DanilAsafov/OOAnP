namespace SpaceBattle.Lib.Tests.Commands;

public class CheckAuthCommandTest : IDisposable
{
    public CheckAuthCommandTest()
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
    public void Execute_PlayerIsOwner_DoesNotThrow()
    {
        var authMock = new Mock<IAuthenticatable>();
        authMock.Setup(a => a.IsOwner("player-1", "ship-1")).Returns(true);
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Auth",
            (object[] args) => authMock.Object
        ).Execute();

        var cmd = new CheckAuthCommand("player-1", "ship-1");

        cmd.Execute();

        authMock.Verify(a => a.IsOwner("player-1", "ship-1"), Times.Once);
    }

    [Fact]
    public void Execute_PlayerIsNotOwner_ThrowsUnauthorizedAccessException()
    {
        var authMock = new Mock<IAuthenticatable>();
        authMock.Setup(a => a.IsOwner("player-1", "ship-1")).Returns(false);
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Auth",
            (object[] args) => authMock.Object
        ).Execute();

        var cmd = new CheckAuthCommand("player-1", "ship-1");

        var ex = Assert.Throws<UnauthorizedAccessException>(() => cmd.Execute());
        Assert.Contains("player-1", ex.Message);
        Assert.Contains("ship-1", ex.Message);
    }
}
