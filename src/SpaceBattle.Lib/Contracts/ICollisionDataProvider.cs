namespace SpaceBattle.Lib.Contracts;

public interface ICollisionDataProvider
{
    bool HasCollision(Vector relativeState);
}
