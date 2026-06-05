namespace SpaceBattle.Lib.Strategies;

public class CollisionTreeBuildStrategy : IStrategy
{
    public object Resolve(params object[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        if (args is not [int gridSize])
            throw new ArgumentException("Ожидается один аргумент типа int.", nameof(args));

        var collisions = new HashSet<Vector>();

        for (int dx = -gridSize; dx <= gridSize; dx++)
            for (int dy = -gridSize; dy <= gridSize; dy++)
                for (int dvx = -gridSize; dvx <= gridSize; dvx++)
                    for (int dvy = -gridSize; dvy <= gridSize; dvy++)
                    {
                        if (WillCollide(dx, dy, dvx, dvy))
                            collisions.Add(new Vector([dx, dy, dvx, dvy]));
                    }

        return collisions;
    }

    private static bool WillCollide(int dx, int dy, int dvx, int dvy)
    {
        if (dx == 0 && dy == 0) return true;
        int newDx = dx + dvx;
        int newDy = dy + dvy;
        return newDx == 0 && newDy == 0;
    }
}
