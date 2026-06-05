namespace SpaceBattle.Lib.Tests.Strategies;

public class CollisionDataFileReaderTest : IDisposable
{
    private readonly string _tempFile = Path.Combine(Path.GetTempPath(), $"collision_read_{Guid.NewGuid()}.csv");

    public void Dispose()
    {
        if (File.Exists(_tempFile))
            File.Delete(_tempFile);
    }

    [Fact]
    public void Resolve_ValidFile_ReturnsCollisionData()
    {
        File.WriteAllLines(_tempFile, ["1,2,3,4", "5,6,7,8"]);
        var reader = new CollisionDataFileReader();

        var result = (CollisionData)reader.Resolve(_tempFile);

        Assert.True(result.HasCollision(new Vector([1, 2, 3, 4])));
        Assert.True(result.HasCollision(new Vector([5, 6, 7, 8])));
        Assert.False(result.HasCollision(new Vector([0, 0, 0, 0])));
    }

    [Fact]
    public void Resolve_RoundTrip_PreservesData()
    {
        var original = new HashSet<Vector> { new Vector([1, 0, -1, 0]), new Vector([0, 1, 0, -1]) };
        new CollisionDataFileWriter().Resolve(_tempFile, original);

        var result = (CollisionData)new CollisionDataFileReader().Resolve(_tempFile);

        Assert.True(result.HasCollision(new Vector([1, 0, -1, 0])));
        Assert.True(result.HasCollision(new Vector([0, 1, 0, -1])));
    }

    [Fact]
    public void Resolve_NullArgs_ThrowsArgumentNullException()
    {
        var reader = new CollisionDataFileReader();

        Assert.Throws<ArgumentNullException>(() => reader.Resolve(null!));
    }

    [Fact]
    public void Resolve_InvalidArgs_ThrowsArgumentException()
    {
        var reader = new CollisionDataFileReader();

        Assert.Throws<ArgumentException>(() => reader.Resolve(123));
    }

    [Fact]
    public void Resolve_TooManyArgs_ThrowsArgumentException()
    {
        var reader = new CollisionDataFileReader();

        Assert.Throws<ArgumentException>(() => reader.Resolve("path1", "path2"));
    }
}
