namespace SpaceBattle.Lib.Commands;

public class StartAction(ICommandReceiver receiver, ICommand command, string operationId, IDictionary<string, object> gameEntity) : ICommand
{
    private readonly ICommandReceiver _receiver = receiver;
    private readonly ICommand _command = command;
    private readonly string _operationId = operationId;
    private readonly IDictionary<string, object> _gameEntity = gameEntity;

    public void Execute()
    {
        _receiver.Receive(_command);
        _gameEntity[_operationId] = _command;
    }
}
