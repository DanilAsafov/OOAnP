namespace SpaceBattle.Lib.Commands;

public class CheckCollisionsCommand(string objectId) : ICommand
{
    private readonly string _objectId = objectId;

    public void Execute()
    {
        var repo = Ioc.Resolve<IGameObjectRepository>("Game.Repository");
        var obj = repo.Get(_objectId);
        var position = (Vector)obj["Position"];

        var area = Ioc.Resolve<GameArea>("Game.Area");
        var nearby = area.GetNearbyObjects(position);

        foreach (var nearbyId in nearby)
        {
            if (nearbyId == _objectId) continue;
            Ioc.Resolve<ICommand>("Game.CollisionDetect", _objectId, nearbyId).Execute();
        }
    }
}
