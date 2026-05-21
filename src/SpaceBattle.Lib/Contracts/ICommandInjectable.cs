namespace SpaceBattle.Lib.Contracts;

public interface ICommandInjectable
{
    void Inject(ICommand command);
}
