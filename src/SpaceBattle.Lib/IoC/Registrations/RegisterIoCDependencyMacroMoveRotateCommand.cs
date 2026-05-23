namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyMacroMoveRotateCommand : ICommand
{
    public void Execute()
    {
        Register("Move");
        Register("Rotate");
    }

    private static void Register(string operation)
    {
        var strategy = new CreateMacroCommandStrategy(operation);

        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            $"Macro.{operation}",
            (object[] args) => strategy.Resolve(args)
        ).Execute();
    }
}
