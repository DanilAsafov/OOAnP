namespace SpaceBattle.Lib.Tests.Models;

public class GameAreaTest
{
    [Fact]
    public void Register_GetNearby_ReturnsRegisteredObject()
    {
        var area = new GameArea(10);
        area.Register("obj-1", new Vector([5, 5]));

        var nearby = area.GetNearbyObjects(new Vector([5, 5]));

        Assert.Contains("obj-1", nearby);
    }

    [Fact]
    public void GetNearbyObjects_AdjacentRegion_ReturnsObject()
    {
        var area = new GameArea(10);
        area.Register("obj-1", new Vector([5, 5]));
        area.Register("obj-2", new Vector([15, 5]));

        var nearby = area.GetNearbyObjects(new Vector([5, 5]));

        Assert.Contains("obj-1", nearby);
        Assert.Contains("obj-2", nearby);
    }

    [Fact]
    public void GetNearbyObjects_FarRegion_DoesNotReturnObject()
    {
        var area = new GameArea(10);
        area.Register("obj-1", new Vector([5, 5]));
        area.Register("obj-far", new Vector([100, 100]));

        var nearby = area.GetNearbyObjects(new Vector([5, 5]));

        Assert.Contains("obj-1", nearby);
        Assert.DoesNotContain("obj-far", nearby);
    }

    [Fact]
    public void Unregister_LastObjectInRegion_RemovesRegion()
    {
        var area = new GameArea(10);
        area.Register("obj-1", new Vector([5, 5]));
        area.Unregister("obj-1", new Vector([5, 5]));

        var nearby = area.GetNearbyObjects(new Vector([5, 5]));

        Assert.DoesNotContain("obj-1", nearby);
    }

    [Fact]
    public void Unregister_OneOfMultiple_KeepsOthers()
    {
        var area = new GameArea(10);
        area.Register("obj-1", new Vector([5, 5]));
        area.Register("obj-2", new Vector([5, 5]));
        area.Unregister("obj-1", new Vector([5, 5]));

        var nearby = area.GetNearbyObjects(new Vector([5, 5]));

        Assert.DoesNotContain("obj-1", nearby);
        Assert.Contains("obj-2", nearby);
    }

    [Fact]
    public void Unregister_NonExistentRegion_DoesNotThrow()
    {
        var area = new GameArea(10);

        var exception = Record.Exception(() => area.Unregister("obj-1", new Vector([5, 5])));

        Assert.Null(exception);
    }

    [Fact]
    public void Constructor_ZeroRegionSize_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new GameArea(0));
    }

    [Fact]
    public void Constructor_NegativeRegionSize_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new GameArea(-1));
    }
}
