using EnadlaInventory.Core.Communication.RequestMessageInfo;
using EnadlaInventory.Core.Communication.ResponseMessage;

namespace EnadlaInventory.Core.Communication
{
    public class RequestMessageSender
    {
        private HttpClient _httpClient;
        private string _host;


        public HttpClient HttpClient
        {
            get => _httpClient;
        }

        public string Host
        {
            get => _host;
        }


        public RequestMessageSender(HttpClient httpClient, string host)
        {
            _httpClient = httpClient;
            _host = host;
        }


        public async Task<ResponseMessageHandlerBase> SendRequestMessageAsync(IRequestMessageInfoHandler requestMessageInfo)
        {
            ResponseMessageHandlerBase? localResponse = TryUseLocalServer(requestMessageInfo.LocalServerResponder);

            if (localResponse is not null)
            {
                return localResponse;
            }

            HttpRequestMessage requestMessage = BuildRequestMessage(requestMessageInfo);

            HttpResponseMessage rawResponse = await _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

            return new NormalResponseMessageHandler(rawResponse);
        }

        private ResponseMessageHandlerBase? TryUseLocalServer(ILocalServerResponder? localServer)
        {
            if (localServer is null)
            {
                return null;
            }

            return localServer.TryGenerateLocalResponse();
        }

        private HttpRequestMessage BuildRequestMessage(IRequestMessageInfoHandler requestMessageInfo)
        {
            RequestHeaderInfo headerInfo = requestMessageInfo.RequestHeaderInfo;

            HttpMethod method = headerInfo.HttpMethod;
            string requestUri = BuildRequestUri(headerInfo.EndPoint);

            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);
            requestMessage.Content = requestMessageInfo.BuildRequestMessageContent();

            return requestMessage;
        }

        private string BuildRequestUri(string endPoint)
        {
            return $"{_host}/{endPoint}";
        }
    }
}
