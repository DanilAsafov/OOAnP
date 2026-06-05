namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyGame : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.Command",
            (object[] args) =>
            {
                if (args is not [IDictionary<string, object> order])
                    throw new ArgumentException("Ожидается один аргумент типа IDictionary<string, object>.", nameof(args));

                return new Game(order);
            }
        ).Execute();
    }
}
