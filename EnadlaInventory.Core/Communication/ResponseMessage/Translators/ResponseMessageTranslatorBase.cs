namespace EnadlaInventory.Core.Communication.ResponseMessage.Translators
{
    public abstract class ResponseMessageTranslatorBase : IDisposable
    {
        protected ResponseMessageHandlerBase _responseMessageHandler;


        public ResponseMessageTranslatorBase(ResponseMessageHandlerBase responseMessageHandler)
        {
            if (responseMessageHandler.IsConsumed)
            {
                throw new ArgumentException("the responseMessageHandler is currently consumed by other translator");
            }

            _responseMessageHandler = responseMessageHandler;
            _responseMessageHandler.Consume();
        }


        public virtual void Dispose()
        {
            _responseMessageHandler.Dispose();
        }
    }
}
