namespace SpaceBattle.Lib.Models;

public readonly struct Angle(int direction)
{
    private const int _directions = 8;
    private readonly int _direction = ((direction % _directions) + _directions) % _directions;

    public static implicit operator double(Angle angle) => (double)angle._direction / _directions * Math.Tau;

    public static Angle operator +(Angle angle1, Angle angle2) => new(angle1._direction + angle2._direction);

    public static bool operator ==(Angle angle1, Angle angle2) => angle1.Equals(angle2);

    public static bool operator !=(Angle angle1, Angle angle2) => !(angle1 == angle2);

    public override bool Equals(object? obj) => obj is Angle angle && _direction == angle._direction;

    public override int GetHashCode() => HashCode.Combine(_direction);
}
