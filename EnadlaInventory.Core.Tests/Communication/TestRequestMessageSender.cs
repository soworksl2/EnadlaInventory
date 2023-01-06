using EnadlaInventory.Core.Communication;
using EnadlaInventory.Core.Communication.RequestMessageInfo;
using EnadlaInventory.Core.Communication.ResponseMessage;
using Moq;
using System.Text;
using MockHttp;

namespace EnadlaInventory.Core.Tests.Communication
{
    [TestClass]
    public class TestRequestMessageSender
    {
        private MockHttpHandler CreateSimpleMockedHttpHandler(string requestUri, HttpMethod method, bool isVerifiable)
        {
            MockHttpHandler httpMessageHandler = new MockHttpHandler();
            var sequenceConfigurationHttp = httpMessageHandler
                .When(matching => matching
                                    .Method(method)
                                    .RequestUri(requestUri)
                )
                .Respond(with => with
                                    .StatusCode(System.Net.HttpStatusCode.OK)
                                    .Body("Test tespond")
                );

            if (isVerifiable)
            {
                sequenceConfigurationHttp.Verifiable();
            }

            return httpMessageHandler;
        }

        private HttpContent CreateGenericDefaultHttpContentForRequest()
        {
            HttpContent output = new ByteArrayContent(Encoding.UTF8.GetBytes("{\"Test\": \"Hello world\"}"));
            output.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return output;
        }


        [TestMethod]
        public async Task Send_request_to_server()
        {
            //Arrange
            RequestHeaderInfo requestHeaderInfo = new RequestHeaderInfo("someEndpoint/", HttpMethod.Post);
            HttpContent content = CreateGenericDefaultHttpContentForRequest();

            RequestMessageInfoHandlerMockedBuilder messageInfoHandlerBuilder = new RequestMessageInfoHandlerMockedBuilder();
            messageInfoHandlerBuilder.AddRequestHeaderInfo(requestHeaderInfo);
            messageInfoHandlerBuilder.AddHttpContent(content);
            IRequestMessageInfoHandler requestMessageInfoHandlerMocked = messageInfoHandlerBuilder.Build();

            MockHttpHandler httpMessageHandler = CreateSimpleMockedHttpHandler("http://www.someHost.com/someEndpoint/", HttpMethod.Post, true);
            HttpClient client = new HttpClient(httpMessageHandler);

            RequestMessageSender sut = new RequestMessageSender(client, "http://www.someHost.com");

            //Act
            ResponseMessageHandlerBase result = await sut.SendRequestMessageAsync(requestMessageInfoHandlerMocked);

            //Assert
            httpMessageHandler.Verify();
            Assert.IsInstanceOfType(result, typeof(NormalResponseMessageHandler));

            NormalResponseMessageHandler resultAsNormalResponse = (NormalResponseMessageHandler)result;

            Assert.AreEqual(content, resultAsNormalResponse.RawResponse.RequestMessage?.Content);
        }

        [TestMethod]
        public async Task Send_request_using_LocalServer()
        {
            //Arrange
            var ResponseMessageHandlerMocker = new Mock<ResponseMessageHandlerBase>();
            ResponseMessageHandlerBase responseMessageHandlerMocked = ResponseMessageHandlerMocker.Object;
            
            RequestMessageInfoHandlerMockedBuilder messageInfoHandlerBuilder = new RequestMessageInfoHandlerMockedBuilder();
            messageInfoHandlerBuilder.AddLocalServer(responseMessageHandlerMocked);
            IRequestMessageInfoHandler requestMessageInfoHandlerMocked = messageInfoHandlerBuilder.Build();
            
            RequestMessageSender sut = new RequestMessageSender(new HttpClient(), "http://www.someHost.com");

            //Act
            ResponseMessageHandlerBase result = await sut.SendRequestMessageAsync(requestMessageInfoHandlerMocked);

            //Assert
            Assert.AreEqual(responseMessageHandlerMocked, result);
        }

        [TestMethod]
        public async Task when_local_server_not_return_response_send_request_to_server()
        {
            //Arrange
            RequestHeaderInfo headerInfo = new RequestHeaderInfo("someEndpoint/", HttpMethod.Post);
            HttpContent requestContent = CreateGenericDefaultHttpContentForRequest();

            RequestMessageInfoHandlerMockedBuilder messageInfohandlerBuilder = new RequestMessageInfoHandlerMockedBuilder();
            messageInfohandlerBuilder.AddLocalServer(null);
            messageInfohandlerBuilder.AddRequestHeaderInfo(headerInfo);
            messageInfohandlerBuilder.AddHttpContent(requestContent);
            IRequestMessageInfoHandler requestMessageInfoHandler = messageInfohandlerBuilder.Build();

            MockHttpHandler httpHandler = CreateSimpleMockedHttpHandler("http://www.someHost.com/someEndpoint/", HttpMethod.Post, true);
            HttpClient client = new HttpClient(httpHandler);

            RequestMessageSender sut = new RequestMessageSender(client, "http://www.someHost.com");

            //Act
            ResponseMessageHandlerBase result = await sut.SendRequestMessageAsync(requestMessageInfoHandler);

            httpHandler.Verify();
        }
    }
}
