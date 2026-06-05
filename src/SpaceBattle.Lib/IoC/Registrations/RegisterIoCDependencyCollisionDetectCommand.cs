namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyCollisionDetectCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.CollisionDetect",
            (object[] args) =>
            {
                if (args.Length < 2 || args[0] is not string objectId1 || args[1] is not string objectId2)
                    throw new ArgumentException("Ожидаются два строковых аргумента: objectId1, objectId2.", nameof(args));

                return new CollisionDetectCommand(objectId1, objectId2);
            }
        ).Execute();
    }
}
