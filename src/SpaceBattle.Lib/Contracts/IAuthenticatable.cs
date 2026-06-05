namespace SpaceBattle.Lib.Contracts;

public interface IAuthenticatable
{
    bool IsOwner(string playerId, string gameObjectId);
}
