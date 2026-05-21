namespace SpaceBattle.Lib.Contracts;

public interface IMovable
{
    Vector Position { get; set; }
    Vector Velocity { get; }
}
