namespace EnadlaInventory.Core.Communication
{
    public sealed class RequestHeaderInfo
    {
        private string _endPoint;
        private HttpMethod _httpMethod;


        public string EndPoint
        {
            get => _endPoint;
        }

        public HttpMethod HttpMethod
        {
            get => _httpMethod;
        }

        public RequestHeaderInfo(string endPoint, HttpMethod httpMethod)
        {
            _endPoint = endPoint;
            _httpMethod = httpMethod;
        }
    }
}
