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

    [Fact]
    public void OperatorMinus_ValidVectors_ReturnsCorrectDifference()
    {
        var vector1 = new Vector([3, 5, 7]);
        var vector2 = new Vector([1, 2, 3]);

        var result = vector1 - vector2;

        Assert.Equal(new Vector([2, 3, 4]), result);
    }

    [Fact]
    public void OperatorMinus_DifferentDimensions_ThrowsArgumentException()
    {
        var vector1 = new Vector([1, 2, 3]);
        var vector2 = new Vector([1, 2]);

        Assert.Throws<ArgumentException>(() => vector1 - vector2);
    }

    [Fact]
    public void OperatorMinus_NullFirstArg_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => (Vector)null! - new Vector([1]));
    }

    [Fact]
    public void OperatorMinus_NullSecondArg_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new Vector([1]) - (Vector)null!);
    }

    [Fact]
    public void Indexer_ValidIndex_ReturnsCoordinate()
    {
        var vector = new Vector([10, 20, 30]);

        Assert.Equal(10, vector[0]);
        Assert.Equal(20, vector[1]);
        Assert.Equal(30, vector[2]);
    }

    [Fact]
    public void Concat_TwoVectors_ReturnsCombined()
    {
        var v1 = new Vector([1, 2]);
        var v2 = new Vector([3, 4]);

        var result = Vector.Concat(v1, v2);

        Assert.Equal(new Vector([1, 2, 3, 4]), result);
    }

    [Fact]
    public void Concat_NullFirstArg_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Vector.Concat(null!, new Vector([1])));
    }

    [Fact]
    public void Concat_NullSecondArg_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => Vector.Concat(new Vector([1]), null!));
    }
}
