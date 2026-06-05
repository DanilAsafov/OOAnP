namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyGameObjectRepository : ICommand
{
    public void Execute()
    {
        var repo = new GameObjectRepository();
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.Repository",
            (object[] args) => repo
        ).Execute();
    }
}
