namespace SpaceBattle.Lib.Tests.Models;

public class GameObjectRepositoryTest
{
    [Fact]
    public void PutAndGet_ValidObject_ReturnsObject()
    {
        var repo = new GameObjectRepository();
        var gameObject = Mock.Of<IGameObject>();

        repo.Put("ship-1", gameObject);
        var result = repo.Get("ship-1");

        Assert.Same(gameObject, result);
    }

    [Fact]
    public void Get_NonExistentId_ThrowsKeyNotFoundException()
    {
        var repo = new GameObjectRepository();

        Assert.Throws<KeyNotFoundException>(() => repo.Get("non-existent"));
    }

    [Fact]
    public void Remove_ExistingObject_RemovesIt()
    {
        var repo = new GameObjectRepository();
        var gameObject = Mock.Of<IGameObject>();
        repo.Put("ship-1", gameObject);

        repo.Remove("ship-1");

        Assert.Throws<KeyNotFoundException>(() => repo.Get("ship-1"));
    }

    [Fact]
    public void Put_SameId_OverwritesPrevious()
    {
        var repo = new GameObjectRepository();
        var obj1 = Mock.Of<IGameObject>();
        var obj2 = Mock.Of<IGameObject>();

        repo.Put("ship-1", obj1);
        repo.Put("ship-1", obj2);

        Assert.Same(obj2, repo.Get("ship-1"));
    }
}
