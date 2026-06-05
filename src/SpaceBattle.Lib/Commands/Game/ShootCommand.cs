namespace SpaceBattle.Lib.Commands;

public class ShootCommand(string shipId, string torpedoId) : ICommand
{
    private readonly string _shipId = shipId;
    private readonly string _torpedoId = torpedoId;

    public void Execute()
    {
        var repo = Ioc.Resolve<IGameObjectRepository>("Game.Repository");
        var ship = repo.Get(_shipId);

        var torpedo = Ioc.Resolve<IGameObject>("Game.CreateTorpedo", ship);
        repo.Put(_torpedoId, torpedo);

        var moveCmd = Ioc.Resolve<ICommand>("Commands.Move", torpedo);
        var receiver = Ioc.Resolve<ICommandReceiver>("Game.CommandReceiver");
        var injectableCmd = Ioc.Resolve<ICommand>("Commands.CommandInjectable", moveCmd);

        var startOrder = new Dictionary<string, object>
        {
            { "Command", injectableCmd },
            { "Receiver", receiver },
            { "OperationId", $"Torpedo.{_torpedoId}" },
            { "Entity", torpedo }
        };
        Ioc.Resolve<ICommand>("Actions.Start", startOrder).Execute();
    }
}
