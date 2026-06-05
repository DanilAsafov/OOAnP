namespace SpaceBattle.Lib.Tests.Commands;

public class CheckCollisionsCommandTest : IDisposable
{
    public CheckCollisionsCommandTest()
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
    public void Execute_HasNearbyObjects_CallsCollisionDetectForEach()
    {
        var objMock = new Mock<IGameObject>();
        objMock.Setup(o => o["Position"]).Returns(new Vector([5, 5]));

        var repoMock = new Mock<IGameObjectRepository>();
        repoMock.Setup(r => r.Get("obj-1")).Returns(objMock.Object);

        var area = new GameArea(10);
        area.Register("obj-1", new Vector([5, 5]));
        area.Register("obj-2", new Vector([5, 5]));
        area.Register("obj-3", new Vector([5, 5]));

        var detectMock = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Repository",
            (object[] args) => repoMock.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Area",
            (object[] args) => area).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CollisionDetect",
            (object[] args) => detectMock.Object).Execute();

        var cmd = new CheckCollisionsCommand("obj-1");
        cmd.Execute();

        detectMock.Verify(c => c.Execute(), Times.Exactly(2));
    }

    [Fact]
    public void Execute_OnlySelfNearby_DoesNotCallCollisionDetect()
    {
        var objMock = new Mock<IGameObject>();
        objMock.Setup(o => o["Position"]).Returns(new Vector([5, 5]));

        var repoMock = new Mock<IGameObjectRepository>();
        repoMock.Setup(r => r.Get("obj-1")).Returns(objMock.Object);

        var area = new GameArea(10);
        area.Register("obj-1", new Vector([5, 5]));

        var detectMock = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Repository",
            (object[] args) => repoMock.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Area",
            (object[] args) => area).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CollisionDetect",
            (object[] args) => detectMock.Object).Execute();

        var cmd = new CheckCollisionsCommand("obj-1");
        cmd.Execute();

        detectMock.Verify(c => c.Execute(), Times.Never);
    }
}
