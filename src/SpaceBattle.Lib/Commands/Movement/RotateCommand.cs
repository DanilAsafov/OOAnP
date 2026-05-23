namespace SpaceBattle.Lib.Commands;

public class RotateCommand(IRotatable obj) : ICommand
{
    private readonly IRotatable _obj = obj;

    public void Execute()
    {
        _obj.Direction += _obj.AngularVelocity;
    }
}
