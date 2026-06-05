namespace SpaceBattle.Lib.Tests.Strategies;

public class CollisionDataFileWriterTest : IDisposable
{
    private readonly string _tempFile = Path.Combine(Path.GetTempPath(), $"collision_write_{Guid.NewGuid()}.csv");

    public void Dispose()
    {
        if (File.Exists(_tempFile))
            File.Delete(_tempFile);
    }

    [Fact]
    public void Resolve_ValidData_WritesFile()
    {
        var collisions = new HashSet<Vector> { new Vector([1, 2, 3, 4]) };
        var writer = new CollisionDataFileWriter();

        writer.Resolve(_tempFile, collisions);

        var lines = File.ReadAllLines(_tempFile);
        Assert.Single(lines);
        Assert.Equal("1,2,3,4", lines[0]);
    }

    [Fact]
    public void Resolve_NullArgs_ThrowsArgumentNullException()
    {
        var writer = new CollisionDataFileWriter();

        Assert.Throws<ArgumentNullException>(() => writer.Resolve(null!));
    }

    [Fact]
    public void Resolve_InvalidArgs_ThrowsArgumentException()
    {
        var writer = new CollisionDataFileWriter();

        Assert.Throws<ArgumentException>(() => writer.Resolve("path"));
    }

    [Fact]
    public void Resolve_InvalidArgsType_ThrowsArgumentException()
    {
        var writer = new CollisionDataFileWriter();

        Assert.Throws<ArgumentException>(() => writer.Resolve(123, 456));
    }
}
