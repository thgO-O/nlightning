namespace NLightning.Bolts.Tests.BOLT2.Payloads;

using Bolts.BOLT2.Payloads;
using Common.Types;

public class TxSignaturesPayloadTests
{
    [Fact]
    public void Given_TxIdLengthOutOfBounds_When_Constructing_Then_ThrowsArgumentException()
    {
        // Arrange
        var channelId = ChannelId.Zero;
        byte[] txId = [0x00, 0x01, 0x02, 0x03];
        var witnesses = new List<Witness>
        {
            new([0xFF, 0xFF, 0xFF, 0xFD])
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TxSignaturesPayload(channelId, txId, witnesses));
    }
}