namespace SpaceBattle.Lib.Commands;

public class CollisionDetectCommand(string objectId1, string objectId2) : ICommand
{
    private readonly string _objectId1 = objectId1;
    private readonly string _objectId2 = objectId2;

    public void Execute()
    {
        var repo = Ioc.Resolve<IGameObjectRepository>("Game.Repository");
        var obj1 = repo.Get(_objectId1);
        var obj2 = repo.Get(_objectId2);

        var pos1 = (Vector)obj1["Position"];
        var pos2 = (Vector)obj2["Position"];
        var vel1 = (Vector)obj1["Velocity"];
        var vel2 = (Vector)obj2["Velocity"];

        var positionDiff = pos2 - pos1;
        var velocityDiff = vel2 - vel1;
        var relativeState = Vector.Concat(positionDiff, velocityDiff);

        var provider = Ioc.Resolve<ICollisionDataProvider>("Game.CollisionData");
        if (provider.HasCollision(relativeState))
        {
            Ioc.Resolve<ICommand>("Game.OnCollision", _objectId1, _objectId2).Execute();
        }
    }
}
