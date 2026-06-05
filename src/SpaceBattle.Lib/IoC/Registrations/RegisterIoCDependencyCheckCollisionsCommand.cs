namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyCheckCollisionsCommand : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.CheckCollisions",
            (object[] args) =>
            {
                if (args is not [string objectId])
                    throw new ArgumentException("Ожидается один аргумент типа string.", nameof(args));

                return new CheckCollisionsCommand(objectId);
            }
        ).Execute();
    }
}
