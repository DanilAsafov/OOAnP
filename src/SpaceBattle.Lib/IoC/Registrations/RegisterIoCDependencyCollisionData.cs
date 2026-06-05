namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyCollisionData(string filePath) : ICommand
{
    private readonly string _filePath = filePath
        ?? throw new ArgumentNullException(nameof(filePath));

    public void Execute()
    {
        var data = (ICollisionDataProvider)new CollisionDataFileReader().Resolve(_filePath);
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.CollisionData",
            (object[] args) => data
        ).Execute();
    }
}
