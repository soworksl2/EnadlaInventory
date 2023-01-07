using EnadlaInventory.Core.Communication;
using Moq;

namespace EnadlaInventory.Core.Tests.Communication
{
    [TestClass]
    public class TestResponseMessageTranslatorBase
    {
        [TestMethod]
        public void Construct_two_ResponseMessageTranslator_with_same_ResponseMessageHandler_throw_ArgumentException()
        {
            var responseMessageHandlerMocker = new Mock<ResponseMessageHandlerBase>();
            ResponseMessageHandlerBase responseMessageHandlerMock = responseMessageHandlerMocker.Object;
            ResponseMessageTranslatorBase rmtb1 = new ResponseMessageTranslatorImplementationSample(responseMessageHandlerMock);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                ResponseMessageTranslatorBase rmtb2 = new ResponseMessageTranslatorImplementationSample(responseMessageHandlerMock);
            });
        }

        [TestMethod]
        public void ResponseMessageTranslator_Dispose()
        {
            var ResponseMessageHandlerMocker = new Mock<ResponseMessageHandlerBase>();
            ResponseMessageHandlerBase messageHandlerMocked = ResponseMessageHandlerMocker.Object;
            ResponseMessageTranslatorImplementationSample messageTranslator = new(messageHandlerMocked);

            messageTranslator.Dispose();

            ResponseMessageHandlerMocker.Verify(x => x.Dispose(), Times.Once);
        }
    }
}
