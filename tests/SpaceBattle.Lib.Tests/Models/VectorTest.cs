namespace SpaceBattle.Lib.Tests.Models;

public class VectorTest
{
    [Fact]
    public void Constructor_NullArray_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Vector(null!));
    }

    [Fact]
    public void OperatorPlus_ValidVectors_ReturnsCorrectSum()
    {
        var vector1 = new Vector([1, -1, 2]);
        var vector2 = new Vector([-1, 1, -2]);
        var expected = new Vector([0, 0, 0]);

        var result = vector1 + vector2;

        Assert.Equal(expected, result);
    }

    [Fact]
    public void OperatorPlus_DifferentDimensions_ThrowsArgumentException()
    {
        var vector1 = new Vector([1, 2, 3]);
        var vector2 = new Vector([1, 2]);

        Assert.Throws<ArgumentException>(() => vector1 + vector2);
    }

    [Fact]
    public void Equals_SameCoordinates_ReturnsTrue()
    {
        var vector1 = new Vector([5, -10]);
        var vector2 = new Vector([5, -10]);

        Assert.True(vector1.Equals(vector2));
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        var vector = new Vector([0]);
        Assert.False(vector.Equals("String"));
    }

    [Fact]
    public void OperatorEquals_BothNull_ReturnsTrue()
    {
        Vector? vector1 = null;
        Vector? vector2 = null;

        Assert.True(vector1 == vector2);
    }

    [Fact]
    public void OperatorNotEquals_DifferentCoordinates_ReturnsTrue()
    {
        var vector1 = new Vector([1, -2]);
        var vector2 = new Vector([-1, 2]);

        Assert.True(vector1 != vector2);
    }

    [Fact]
    public void GetHashCode_SameCoordinates_ReturnsSameHash()
    {
        var vector1 = new Vector([1, -2]);
        var vector2 = new Vector([1, -2]);

        Assert.Equal(vector1.GetHashCode(), vector2.GetHashCode());
    }
}
