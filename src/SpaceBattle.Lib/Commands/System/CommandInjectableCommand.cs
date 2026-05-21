namespace SpaceBattle.Lib.Commands;

public class CommandInjectableCommand : ICommand, ICommandInjectable
{
    private ICommand? _injectableCommand;

    public void Inject(ICommand command) => _injectableCommand = command;

    public void Execute()
    {
        if (_injectableCommand is null) throw new InvalidOperationException();

        _injectableCommand.Execute();
    }
}
