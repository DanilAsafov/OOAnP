namespace SpaceBattle.Lib.Tests.Commands;

public class CollisionDetectCommandTest : IDisposable
{
    public CollisionDetectCommandTest()
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
    public void Execute_CollisionDetected_CallsOnCollision()
    {
        var obj1Mock = new Mock<IGameObject>();
        obj1Mock.Setup(o => o["Position"]).Returns(new Vector([0, 0]));
        obj1Mock.Setup(o => o["Velocity"]).Returns(new Vector([1, 0]));

        var obj2Mock = new Mock<IGameObject>();
        obj2Mock.Setup(o => o["Position"]).Returns(new Vector([1, 0]));
        obj2Mock.Setup(o => o["Velocity"]).Returns(new Vector([0, 0]));

        var repoMock = new Mock<IGameObjectRepository>();
        repoMock.Setup(r => r.Get("obj-1")).Returns(obj1Mock.Object);
        repoMock.Setup(r => r.Get("obj-2")).Returns(obj2Mock.Object);

        var providerMock = new Mock<ICollisionDataProvider>();
        providerMock.Setup(p => p.HasCollision(new Vector(new int[] { 1, 0, -1, 0 }))).Returns(true);

        var onCollisionMock = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Repository",
            (object[] args) => repoMock.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CollisionData",
            (object[] args) => providerMock.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.OnCollision",
            (object[] args) => onCollisionMock.Object).Execute();

        var cmd = new CollisionDetectCommand("obj-1", "obj-2");
        cmd.Execute();

        onCollisionMock.Verify(c => c.Execute(), Times.Once);
    }

    [Fact]
    public void Execute_NoCollision_DoesNotCallOnCollision()
    {
        var obj1Mock = new Mock<IGameObject>();
        obj1Mock.Setup(o => o["Position"]).Returns(new Vector([0, 0]));
        obj1Mock.Setup(o => o["Velocity"]).Returns(new Vector([0, 0]));

        var obj2Mock = new Mock<IGameObject>();
        obj2Mock.Setup(o => o["Position"]).Returns(new Vector([10, 10]));
        obj2Mock.Setup(o => o["Velocity"]).Returns(new Vector([0, 0]));

        var repoMock = new Mock<IGameObjectRepository>();
        repoMock.Setup(r => r.Get("obj-1")).Returns(obj1Mock.Object);
        repoMock.Setup(r => r.Get("obj-2")).Returns(obj2Mock.Object);

        var providerMock = new Mock<ICollisionDataProvider>();
        providerMock.Setup(p => p.HasCollision(It.IsAny<Vector>())).Returns(false);

        var onCollisionMock = new Mock<ICommand>();

        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.Repository",
            (object[] args) => repoMock.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.CollisionData",
            (object[] args) => providerMock.Object).Execute();
        Ioc.Resolve<App.ICommand>("IoC.Register", "Game.OnCollision",
            (object[] args) => onCollisionMock.Object).Execute();

        var cmd = new CollisionDetectCommand("obj-1", "obj-2");
        cmd.Execute();

        onCollisionMock.Verify(c => c.Execute(), Times.Never);
    }
}
