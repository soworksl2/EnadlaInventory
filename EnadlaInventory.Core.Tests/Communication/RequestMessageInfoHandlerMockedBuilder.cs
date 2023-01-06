using EnadlaInventory.Core.Communication.RequestMessageInfo;
using EnadlaInventory.Core.Communication.ResponseMessage;
using Moq;

namespace EnadlaInventory.Core.Tests.Communication
{
    internal sealed class RequestMessageInfoHandlerMockedBuilder
    {
        private Mock<IRequestMessageInfoHandler> _mocker = new Mock<IRequestMessageInfoHandler>();


        public void AddLocalServer(ResponseMessageHandlerBase? localServerResponse)
        {
            var localServerResponderMocker = new Mock<ILocalServerResponder>();
            localServerResponderMocker.Setup(o => o.TryGenerateLocalResponse())
                .Returns(localServerResponse);

            ILocalServerResponder localServerResponderMocked = localServerResponderMocker.Object;
            _mocker.Setup(o => o.LocalServerResponder)
                .Returns(localServerResponderMocked);
        }

        public void AddRequestHeaderInfo(RequestHeaderInfo requestHeaderInfo)
        {
            _mocker.Setup(o => o.RequestHeaderInfo).
                Returns(requestHeaderInfo);
        }

        public void AddHttpContent(HttpContent content)
        {
            _mocker.Setup(o => o.BuildRequestMessageContent())
                .Returns(content);
        }

        public IRequestMessageInfoHandler Build()
        {
            return _mocker.Object;
        }
    }
}
