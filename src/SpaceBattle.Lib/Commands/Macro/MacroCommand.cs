namespace SpaceBattle.Lib.Commands;

public class MacroCommand(ICommand[] commands) : ICommand
{
    private readonly ICommand[] _commands = commands;

    public void Execute()
    {
        _commands.ToList().ForEach(command => command.Execute());
    }
}
