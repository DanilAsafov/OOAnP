namespace SpaceBattle.Lib.Models;

public class Vector(int[] coord)
{
    private readonly int[] coordinates = coord ?? throw new ArgumentNullException(nameof(coord));

    public int Dimension => coordinates.Length;

    public override bool Equals(object? obj) => obj is Vector vector && coordinates.SequenceEqual(vector.coordinates);
    

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var c in coordinates)
        {
            hash.Add(c);
        }

        return hash.ToHashCode();
    }

    public static Vector operator +(Vector? vector1, Vector? vector2)
    {
        ArgumentNullException.ThrowIfNull(vector1);
        ArgumentNullException.ThrowIfNull(vector2);

        if (vector1.Dimension != vector2.Dimension) throw new ArgumentException("Размерности векторов должны совпадать.");

        return new Vector([.. vector1.coordinates.Zip(vector2.coordinates, (v1, v2) => v1 + v2)]);
    }

    public static bool operator ==(Vector? vector1, Vector? vector2)
    {
        if (vector1 is null) return vector2 is null;
        return vector1.Equals(vector2);
    }

    public static bool operator !=(Vector? vector1, Vector? vector2) => !(vector1 == vector2);
}
