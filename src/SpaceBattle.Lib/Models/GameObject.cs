namespace SpaceBattle.Lib.Models;

public class GameObject(IDictionary<string, object> properties) : IGameObject
{
    private readonly IDictionary<string, object> _properties = properties
        ?? throw new ArgumentNullException(nameof(properties));

    public object this[string key]
    {
        get => _properties[key];
        set => _properties[key] = value;
    }

    public bool TryGetValue(string key, out object value) => _properties.TryGetValue(key, out value!);
}
