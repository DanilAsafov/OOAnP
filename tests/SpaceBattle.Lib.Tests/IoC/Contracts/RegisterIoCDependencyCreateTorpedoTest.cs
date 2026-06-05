namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyCreateTorpedoTest : IDisposable
{
    public RegisterIoCDependencyCreateTorpedoTest()
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
    public void Execute_ValidShip_ReturnsTorpedoWithShipProperties()
    {
        var shipMock = new Mock<IGameObject>();
        var position = new Vector([10, 20]);
        var velocity = new Vector([1, 0]);
        shipMock.Setup(s => s["Position"]).Returns(position);
        shipMock.Setup(s => s["Velocity"]).Returns(velocity);

        var registration = new RegisterIoCDependencyCreateTorpedo();
        registration.Execute();

        var torpedo = Ioc.Resolve<IGameObject>("Game.CreateTorpedo", shipMock.Object);

        Assert.IsType<GameObject>(torpedo);
        Assert.Equal(position, torpedo["Position"]);
        Assert.Equal(velocity, torpedo["Velocity"]);
    }

    [Fact]
    public void Execute_InvalidArgument_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCreateTorpedo();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<IGameObject>("Game.CreateTorpedo", "InvalidType"));
    }

    [Fact]
    public void Execute_NoArguments_ThrowsArgumentException()
    {
        var registration = new RegisterIoCDependencyCreateTorpedo();
        registration.Execute();

        Assert.Throws<ArgumentException>(() => Ioc.Resolve<IGameObject>("Game.CreateTorpedo"));
    }
}
