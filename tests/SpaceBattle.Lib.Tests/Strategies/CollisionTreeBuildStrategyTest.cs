namespace SpaceBattle.Lib.Tests.Strategies;

public class CollisionTreeBuildStrategyTest
{
    [Fact]
    public void Resolve_GridSize1_Returns17Collisions()
    {
        var strategy = new CollisionTreeBuildStrategy();

        var result = (HashSet<Vector>)strategy.Resolve(1);

        Assert.Equal(17, result.Count);
    }

    [Fact]
    public void Resolve_GridSize1_ContainsSamePositionCollision()
    {
        var strategy = new CollisionTreeBuildStrategy();

        var result = (HashSet<Vector>)strategy.Resolve(1);

        Assert.Contains(new Vector([0, 0, 1, 1]), result);
    }

    [Fact]
    public void Resolve_GridSize1_ContainsNextStepCollision()
    {
        var strategy = new CollisionTreeBuildStrategy();

        var result = (HashSet<Vector>)strategy.Resolve(1);

        Assert.Contains(new Vector([1, 0, -1, 0]), result);
    }

    [Fact]
    public void Resolve_GridSize1_DoesNotContainNoCollision()
    {
        var strategy = new CollisionTreeBuildStrategy();

        var result = (HashSet<Vector>)strategy.Resolve(1);

        Assert.DoesNotContain(new Vector([1, 1, 0, 0]), result);
    }

    [Fact]
    public void Resolve_NullArgs_ThrowsArgumentNullException()
    {
        var strategy = new CollisionTreeBuildStrategy();

        Assert.Throws<ArgumentNullException>(() => strategy.Resolve(null!));
    }

    [Fact]
    public void Resolve_InvalidArgs_ThrowsArgumentException()
    {
        var strategy = new CollisionTreeBuildStrategy();

        Assert.Throws<ArgumentException>(() => strategy.Resolve("invalid"));
    }
}
