namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyStopAction : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Actions.Stop",
            (object[] args) =>
            {
                if (args is not [IDictionary<string, object> order])
                    throw new ArgumentException("Ожидается ровно один аргумент типа IDictionary<string, object>.", nameof(args));

                if (!order.TryGetValue("OperationId", out var operationIdObject) || operationIdObject is not string operationId)
                    throw new ArgumentException("В приказе отсутствует или неверен параметр 'OperationId'.");

                if (!order.TryGetValue("Entity", out var gameEntityObject) || gameEntityObject is not IDictionary<string, object> gameEntity)
                    throw new ArgumentException("В приказе отсутствует или неверен параметр 'Entity'.");

                return new StopAction(operationId, gameEntity);
            }
        ).Execute();
    }
}
