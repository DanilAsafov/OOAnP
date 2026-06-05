namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyShootCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.Actions.Shoot",
            (object[] args) =>
            {
                if (args is not [IDictionary<string, object> order])
                    throw new ArgumentException("Ожидается один аргумент типа IDictionary<string, object>.", nameof(args));

                if (!order.TryGetValue("GameObjectId", out var gameObjectIdObj) || gameObjectIdObj is not string shipId)
                    throw new ArgumentException("В приказе отсутствует или неверен параметр 'GameObjectId'.");

                var torpedoId = $"torpedo-{Guid.NewGuid()}";
                return new ShootCommand(shipId, torpedoId);
            }
        ).Execute();
    }
}
