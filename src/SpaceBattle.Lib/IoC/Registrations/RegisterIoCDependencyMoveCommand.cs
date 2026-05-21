namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyMoveCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Move",
            (object[] args) => 
            new MoveCommand(
                Ioc.Resolve<IMovable>("Adapters.IMovable", args[0])
            )
        ).Execute();
    }
}
