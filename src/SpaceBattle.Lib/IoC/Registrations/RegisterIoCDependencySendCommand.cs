namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencySendCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Commands.Send",
            (object[] args) =>
            {
                if (args.Length != 2)
                {
                    throw new ArgumentException("Ожидается ровно два аргумента: ICommand и ICommandReceiver.", nameof(args));
                }

                if (args[0] is not ICommand command)
                {
                    throw new ArgumentException("Первый аргумент должен быть типа ICommand.");
                }

                if (args[1] is not ICommandReceiver receiver)
                {
                    throw new ArgumentException("Второй аргумент должен быть типа ICommandReceiver.");
                }

                return new SendCommand(command, receiver);
            }
        ).Execute();
    }
}
