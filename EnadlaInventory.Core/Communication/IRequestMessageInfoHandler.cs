namespace EnadlaInventory.Core.Communication
{
    public interface IRequestMessageInfoHandler
    {
        public RequestHeaderInfo RequestHeaderInfo { get; }
        public ILocalServerResponder? LocalServerResponder { get; }


        public HttpContent BuildRequestMessageContent();
    }
}
