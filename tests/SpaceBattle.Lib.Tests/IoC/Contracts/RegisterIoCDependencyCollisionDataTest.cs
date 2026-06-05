namespace SpaceBattle.Lib.Tests.IoC;

public class RegisterIoCDependencyCollisionDataTest : IDisposable
{
    private readonly string _tempFile = Path.Combine(Path.GetTempPath(), $"collision_ioc_{Guid.NewGuid()}.csv");

    public RegisterIoCDependencyCollisionDataTest()
    {
        new InitCommand().Execute();
        var scope = Ioc.Resolve<object>("IoC.Scope.Create");
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Set", scope).Execute();

        File.WriteAllLines(_tempFile, ["1,0,-1,0"]);
    }

    public void Dispose()
    {
        Ioc.Resolve<App.ICommand>("IoC.Scope.Current.Clear").Execute();
        if (File.Exists(_tempFile))
            File.Delete(_tempFile);
    }

    [Fact]
    public void Execute_ValidFile_ReturnsCollisionData()
    {
        var registration = new RegisterIoCDependencyCollisionData(_tempFile);
        registration.Execute();

        var data = Ioc.Resolve<ICollisionDataProvider>("Game.CollisionData");

        Assert.IsType<CollisionData>(data);
    }

    [Fact]
    public void Execute_ResolveTwice_ReturnsSameInstance()
    {
        var registration = new RegisterIoCDependencyCollisionData(_tempFile);
        registration.Execute();

        var data1 = Ioc.Resolve<ICollisionDataProvider>("Game.CollisionData");
        var data2 = Ioc.Resolve<ICollisionDataProvider>("Game.CollisionData");

        Assert.Same(data1, data2);
    }

    [Fact]
    public void Constructor_NullFilePath_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new RegisterIoCDependencyCollisionData(null!));
    }
}
