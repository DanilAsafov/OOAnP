namespace SpaceBattle.Lib.Commands;

public class SendCommand(ICommand command, ICommandReceiver receiver) : ICommand
{
    public void Execute()
    {
        receiver.Receive(command);
    }
}
