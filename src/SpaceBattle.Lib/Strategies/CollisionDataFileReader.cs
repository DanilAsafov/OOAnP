namespace SpaceBattle.Lib.Strategies;

public class CollisionDataFileReader : IStrategy
{
    public object Resolve(params object[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (args is not [string filePath])
            throw new ArgumentException("Ожидается один аргумент типа string.", nameof(args));

        var collisions = new HashSet<Vector>();
        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split(',').Select(int.Parse).ToArray();
            collisions.Add(new Vector(parts));
        }

        return new CollisionData(collisions);
    }
}
