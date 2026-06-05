namespace SpaceBattle.Lib.Models;

public class GameObjectRepository : IGameObjectRepository
{
    private readonly Dictionary<string, IGameObject> _storage = new();

    public IGameObject Get(string id) =>
        _storage.TryGetValue(id, out var obj) ? obj
            : throw new KeyNotFoundException($"Игровой объект '{id}' не найден.");

    public void Put(string id, IGameObject obj) => _storage[id] = obj;

    public void Remove(string id) => _storage.Remove(id);
}
