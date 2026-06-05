namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyGameArea(int regionSize) : ICommand
{
    private readonly int _regionSize = regionSize;

    public void Execute()
    {
        var area = new GameArea(_regionSize);
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.Area",
            (object[] args) => area
        ).Execute();
    }
}
