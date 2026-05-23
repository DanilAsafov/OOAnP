namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyCommandInjectableCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.CommandInjectable",
            (object[] args) => 
            {
                var injectableCommand = new CommandInjectableCommand();
                if (args.Length > 0)
                {
                    if (args is not [ICommand command]) throw new ArgumentException("Аргумент должен быть ровно один и типа ICommand.", nameof(args));
                    injectableCommand.Inject(command);
                }
                   
                return injectableCommand;
            }
        ).Execute();
    }
}
