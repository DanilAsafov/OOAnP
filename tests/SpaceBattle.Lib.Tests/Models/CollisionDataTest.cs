namespace SpaceBattle.Lib.Tests.Models;

public class CollisionDataTest
{
    [Fact]
    public void HasCollision_VectorInSet_ReturnsTrue()
    {
        var collisions = new HashSet<Vector> { new Vector([1, 2, 3, 4]) };
        var data = new CollisionData(collisions);

        Assert.True(data.HasCollision(new Vector([1, 2, 3, 4])));
    }

    [Fact]
    public void HasCollision_VectorNotInSet_ReturnsFalse()
    {
        var collisions = new HashSet<Vector> { new Vector([1, 2, 3, 4]) };
        var data = new CollisionData(collisions);

        Assert.False(data.HasCollision(new Vector([5, 6, 7, 8])));
    }

    [Fact]
    public void HasCollision_NullVector_ThrowsArgumentNullException()
    {
        var data = new CollisionData(new HashSet<Vector>());

        Assert.Throws<ArgumentNullException>(() => data.HasCollision(null!));
    }

    [Fact]
    public void Constructor_NullCollisions_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new CollisionData(null!));
    }
}
