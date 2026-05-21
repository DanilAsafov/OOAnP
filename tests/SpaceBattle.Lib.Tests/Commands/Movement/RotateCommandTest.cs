namespace SpaceBattle.Lib.Tests.Commands;

public class RotateCommandTest
{
    [Fact]
    public void Execute_ValidDirectionAndAngularVelocity_UpdatesDirection()
    {
        var gameEntityMock = new Mock<IRotatable>();
        gameEntityMock.SetupGet(a => a.Direction).Returns(new Angle(1));
        gameEntityMock.SetupGet(a => a.AngularVelocity).Returns(new Angle(1));
        var rotateCommand = new RotateCommand(gameEntityMock.Object);

        rotateCommand.Execute();

        gameEntityMock.VerifySet(a => a.Direction = new Angle(2));
    }

    [Fact]
    public void Execute_DirectionCannotBeRead_ThrowsInvalidOperationException()
    {
        var gameEntityMock = new Mock<IRotatable>();
        gameEntityMock.SetupGet(a => a.Direction).Throws<InvalidOperationException>();
        var rotateCommand = new RotateCommand(gameEntityMock.Object);

        Assert.Throws<InvalidOperationException>(rotateCommand.Execute);
    }

    [Fact]
    public void Execute_AngularVelocityCannotBeRead_ThrowsInvalidOperationException()
    {
        var gameEntityMock = new Mock<IRotatable>();
        gameEntityMock.SetupGet(a => a.AngularVelocity).Throws<InvalidOperationException>();
        var rotateCommand = new RotateCommand(gameEntityMock.Object);

        Assert.Throws<InvalidOperationException>(rotateCommand.Execute);
    }

    [Fact]
    public void Execute_DirectionCannotBeChanged_ThrowsInvalidOperationException()
    {
        var gameEntityMock = new Mock<IRotatable>();
        gameEntityMock.SetupGet(a => a.Direction).Returns(new Angle(1));
        gameEntityMock.SetupGet(a => a.AngularVelocity).Returns(new Angle(1));
        gameEntityMock.SetupSet(a => a.Direction = new Angle(2)).Throws<InvalidOperationException>();
        var rotateCommand = new RotateCommand(gameEntityMock.Object);

        Assert.Throws<InvalidOperationException>(rotateCommand.Execute);
    }
}
