namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyCheckAuthCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.CheckAuth",
            (object[] args) =>
            {
                if (args.Length < 2 || args[0] is not string playerId || args[1] is not string gameObjectId)
                    throw new ArgumentException("Ожидаются два строковых аргумента: playerId, gameObjectId.", nameof(args));

                return new CheckAuthCommand(playerId, gameObjectId);
            }
        ).Execute();
    }
}
