namespace SpaceBattle.Lib.Commands;

public class MoveCommand(IMovable obj) : ICommand
{
    private readonly IMovable _obj = obj;

    public void Execute()
    {
        _obj.Position += _obj.Velocity;
    }
}
