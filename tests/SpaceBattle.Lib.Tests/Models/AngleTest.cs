namespace SpaceBattle.Lib.Tests.Models;

public class AngleTest
{
    [Fact]
    public void OperatorPlus_ValidAngles_ReturnsCorrectSum()
    {
        var angle1 = new Angle(5);
        var angle2 = new Angle(7);
        var expectedSum = new Angle(4);

        var angleSum = angle1 + angle2;

        Assert.Equal(expectedSum, angleSum);
    }

    [Fact]
    public void Equals_SameNormalizedValue_ReturnsTrue()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);

        Assert.True(angle1.Equals(angle2));
    }

    [Fact]
    public void Equals_NullObject_ReturnsFalse()
    {
        var angle1 = new Angle(90);

        Assert.False(angle1.Equals(null));
    }

    [Fact]
    public void EqualityOperator_SameNormalizedValue_ReturnsTrue()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);

        Assert.True(angle1 == angle2);
    }

    [Fact]
    public void OperatorNotEqual_DifferentValues_ReturnsTrue()
    {
        var angle1 = new Angle(1);
        var angle2 = new Angle(2);

        Assert.True(angle1 != angle2);
    }

    [Fact]
    public void ImplicitOperatorDouble_ZeroAngle_ReturnsCorrectValue()
    {
        var angle = new Angle(0);

        double radians = angle;

        Assert.Equal(0.0, radians);
    }

    [Fact]
    public void GetHashCode_SameNormalizedValue_ReturnsSameHash()
    {
        var angle1 = new Angle(15);
        var angle2 = new Angle(23);

        var hash1 = angle1.GetHashCode();
        var hash2 = angle2.GetHashCode();

        Assert.Equal(hash1, hash2);
    }
}
