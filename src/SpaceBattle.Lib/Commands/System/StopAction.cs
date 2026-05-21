namespace SpaceBattle.Lib.Commands;

public class StopAction(string operationId, IDictionary<string, object> gameEntity) : ICommand
{
    private readonly string _operationId = operationId;
    private readonly IDictionary<string, object> _gameEntity = gameEntity;

    public void Execute()
    {
        if (!_gameEntity.Remove(_operationId)) return;
    }
}
