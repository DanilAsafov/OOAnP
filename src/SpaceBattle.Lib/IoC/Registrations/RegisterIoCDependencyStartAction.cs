namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyStartAction : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Actions.Start",
            (object[] args) =>
            {
                if (args is not [IDictionary<string, object> order])
                    throw new ArgumentException("Ожидается ровно один аргумент типа IDictionary<string, object>.", nameof(args));

                if (!order.TryGetValue("Receiver", out var receiverObject) || receiverObject is not ICommandReceiver receiver)
                    throw new ArgumentException("В приказе отсутствует или неверен параметр 'Receiver'.");

                if (!order.TryGetValue("Command", out var commandObject) || commandObject is not ICommand command)
                    throw new ArgumentException("В приказе отсутствует или неверен параметр 'Command'.");

                if (!order.TryGetValue("OperationId", out var operationIdObject) || operationIdObject is not string operationId)
                    throw new ArgumentException("В приказе отсутствует или неверен параметр 'OperationId'.");

                if (!order.TryGetValue("Entity", out var gameEntityObject) || gameEntityObject is not IDictionary<string, object> gameEntity)
                    throw new ArgumentException("В приказе отсутствует или неверен параметр 'Entity'.");

                return new StartAction(receiver, command, operationId, gameEntity);
            }
        ).Execute();
    }
}
