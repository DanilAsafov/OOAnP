namespace SpaceBattle.Lib.Models;

public class CollisionData(HashSet<Vector> collisions) : ICollisionDataProvider
{
    private readonly HashSet<Vector> _collisions = collisions
        ?? throw new ArgumentNullException(nameof(collisions));

    public bool HasCollision(Vector relativeState)
    {
        ArgumentNullException.ThrowIfNull(relativeState);
        return _collisions.Contains(relativeState);
    }
}
