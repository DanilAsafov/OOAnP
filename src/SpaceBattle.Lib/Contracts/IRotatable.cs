namespace SpaceBattle.Lib.Contracts;

public interface IRotatable
{
    Angle Direction { get; set; }
    Angle AngularVelocity { get; }
}
