namespace SpaceBattle.Lib.Contracts;

public interface IGameObject
{
    object this[string key] { get; set; }
    bool TryGetValue(string key, out object value);
}
