namespace SpaceBattle.Lib.Commands;

public class CheckAuthCommand(string playerId, string gameObjectId) : ICommand
{
    private readonly string _playerId = playerId;
    private readonly string _gameObjectId = gameObjectId;

    public void Execute()
    {
        var auth = Ioc.Resolve<IAuthenticatable>("Game.Auth");
        if (!auth.IsOwner(_playerId, _gameObjectId))
            throw new UnauthorizedAccessException(
                $"Игрок '{_playerId}' не имеет права управлять объектом '{_gameObjectId}'.");
    }
}
