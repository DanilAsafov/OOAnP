namespace SpaceBattle.Lib.Tests.Commands;

public class SendCommandTest
{
    [Fact]
    public void Execute_ValidState_PassesCommandToReceiver()
    {
        var sendCommandMock = new Mock<ICommand>();
        var receiverMock = new Mock<ICommandReceiver>();
        var sendCommand = new SendCommand(sendCommandMock.Object, receiverMock.Object);

        sendCommand.Execute();

        receiverMock.Verify(r => r.Receive(sendCommandMock.Object), Times.Once);
    }

    [Fact]
    public void Execute_ReceiverCannotReceiveCommand_ThrowsInvalidOperationException()
    {
        var sendCommandMock = new Mock<ICommand>();
        var receiverMock = new Mock<ICommandReceiver>();
        var sendCommand = new SendCommand(sendCommandMock.Object, receiverMock.Object);

        receiverMock.Setup(r => r.Receive(sendCommandMock.Object)).Throws<InvalidOperationException>();
        
        Assert.Throws<InvalidOperationException>(sendCommand.Execute);
    }
}