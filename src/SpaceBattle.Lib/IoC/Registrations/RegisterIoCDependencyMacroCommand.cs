namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyMacroCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Macro",
            (object[] args) => 
            {
                if (args.Length == 0) return new MacroCommand([]);
                if (args is not [ICommand[] commands]) throw new ArgumentException("Аргумент должен быть ровно один и типа ICommand[].", nameof(args));
                return new MacroCommand(commands);
            }
        ).Execute();
    }
}
