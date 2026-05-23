namespace SpaceBattle.Lib.Tests.Commands;

public class MoveCommandTest
{
    [Fact]
    public void Execute_ValidPositionAndVelocity_UpdatesPosition()
    {
        var gameEntityMock = new Mock<IMovable>();
        gameEntityMock.SetupGet(a => a.Position).Returns(new Vector([12, 5]));
        gameEntityMock.SetupGet(a => a.Velocity).Returns(new Vector([-4, 1]));
        var moveCommand = new MoveCommand(gameEntityMock.Object);
        
        moveCommand.Execute();

        gameEntityMock.VerifySet(a => a.Position = new Vector([8, 6]));
    }

    [Fact]
    public void Execute_PositionCannotBeRead_ThrowsInvalidOperationException()
    {
        var gameEntityMock = new Mock<IMovable>();
        gameEntityMock.SetupGet(a => a.Position).Throws<InvalidOperationException>();
        var moveCommand = new MoveCommand(gameEntityMock.Object);

        Assert.Throws<InvalidOperationException>(moveCommand.Execute);
    }

    [Fact]
    public void Execute_VelocityCannotBeRead_ThrowsInvalidOperationException()
    {
        var gameEntityMock = new Mock<IMovable>();
        gameEntityMock.SetupGet(a => a.Velocity).Throws<InvalidOperationException>();
        var moveCommand = new MoveCommand(gameEntityMock.Object);

        Assert.Throws<InvalidOperationException>(moveCommand.Execute);
    }

    [Fact]
    public void Execute_PositionCannotBeChanged_ThrowsInvalidOperationException()
    {
        var gameEntityMock = new Mock<IMovable>();
        gameEntityMock.SetupGet(a => a.Position).Returns(new Vector([12, 5]));
        gameEntityMock.SetupGet(a => a.Velocity).Returns(new Vector([-4, 1]));
        gameEntityMock.SetupSet(a => a.Position = new Vector([8, 6])).Throws<InvalidOperationException>();
        var moveCommand = new MoveCommand(gameEntityMock.Object);

        Assert.Throws<InvalidOperationException>(moveCommand.Execute);
    }
}
