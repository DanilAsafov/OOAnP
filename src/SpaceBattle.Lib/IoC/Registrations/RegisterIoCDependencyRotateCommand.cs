namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyRotateCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Rotate",
            (object[] args) => 
            new RotateCommand(
                Ioc.Resolve<IRotatable>("Adapters.IRotatable", args[0])
            )
        ).Execute();
    }
}
