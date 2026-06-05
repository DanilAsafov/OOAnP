namespace SpaceBattle.Lib.Strategies;

public class CollisionDataFileWriter : IStrategy
{
    public object Resolve(params object[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (args.Length < 2 || args[0] is not string filePath || args[1] is not HashSet<Vector> collisions)
            throw new ArgumentException("Ожидаются два аргумента: string filePath, HashSet<Vector> collisions.", nameof(args));

        using var writer = new StreamWriter(filePath);
        foreach (var vector in collisions)
        {
            var parts = new int[vector.Dimension];
            for (int i = 0; i < vector.Dimension; i++)
                parts[i] = vector[i];
            writer.WriteLine(string.Join(",", parts));
        }

        return filePath;
    }
}
