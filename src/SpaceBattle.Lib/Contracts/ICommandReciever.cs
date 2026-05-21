namespace SpaceBattle.Lib.Contracts;

public interface ICommandReceiver
{
    void Receive(ICommand command);
}
