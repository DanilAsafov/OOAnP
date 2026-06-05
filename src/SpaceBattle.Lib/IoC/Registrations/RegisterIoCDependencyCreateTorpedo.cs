namespace SpaceBattle.Lib.IoC;

public class RegisterIoCDependencyCreateTorpedo : ICommand
{
    public void Execute()
    {
        Ioc.Resolve<App.ICommand>(
            "IoC.Register",
            "Game.CreateTorpedo",
            (object[] args) =>
            {
                if (args is not [IGameObject ship])
                    throw new ArgumentException("Ожидается один аргумент типа IGameObject.", nameof(args));

                var torpedoProps = new Dictionary<string, object>
                {
                    { "Position", ship["Position"] },
                    { "Velocity", ship["Velocity"] }
                };
                return new GameObject(torpedoProps);
            }
        ).Execute();
    }
}
