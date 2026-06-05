namespace SpaceBattle.Lib.Tests.Models;

public class GameObjectTest
{
    [Fact]
    public void Indexer_SetAndGet_ReturnsValue()
    {
        var props = new Dictionary<string, object>();
        var gameObject = new GameObject(props);

        gameObject["Position"] = new Vector([1, 2]);

        Assert.Equal(new Vector([1, 2]), gameObject["Position"]);
    }

    [Fact]
    public void Indexer_GetMissingKey_ThrowsKeyNotFoundException()
    {
        var props = new Dictionary<string, object>();
        var gameObject = new GameObject(props);

        Assert.Throws<KeyNotFoundException>(() => gameObject["NonExistent"]);
    }

    [Fact]
    public void TryGetValue_ExistingKey_ReturnsTrueAndValue()
    {
        var props = new Dictionary<string, object> { { "Health", 100 } };
        var gameObject = new GameObject(props);

        var result = gameObject.TryGetValue("Health", out var value);

        Assert.True(result);
        Assert.Equal(100, value);
    }

    [Fact]
    public void TryGetValue_MissingKey_ReturnsFalse()
    {
        var props = new Dictionary<string, object>();
        var gameObject = new GameObject(props);

        var result = gameObject.TryGetValue("Missing", out _);

        Assert.False(result);
    }

    [Fact]
    public void Constructor_NullProperties_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new GameObject(null!));
    }
}
