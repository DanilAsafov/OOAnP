namespace SpaceBattle.Lib.Contracts;

public interface IGameObjectRepository
{
    IGameObject Get(string id);
    void Put(string id, IGameObject obj);
    void Remove(string id);
}
