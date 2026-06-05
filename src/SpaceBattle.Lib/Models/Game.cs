namespace SpaceBattle.Lib.Models;

public class Game : ICommand
{
    private readonly IDictionary<string, object> _order;

    public Game(IDictionary<string, object> order)
    {
        _order = order ?? throw new ArgumentNullException(nameof(order));
    }

    public void Execute()
    {
        var playerId = (string)_order["PlayerId"];
        var gameObjectId = (string)_order["GameObjectId"];
        var action = (string)_order["Action"];

        // Авторизация
        Ioc.Resolve<ICommand>("Game.CheckAuth", playerId, gameObjectId).Execute();

        // Выполнение действия
        Ioc.Resolve<ICommand>($"Game.Actions.{action}", _order).Execute();
    }
}
