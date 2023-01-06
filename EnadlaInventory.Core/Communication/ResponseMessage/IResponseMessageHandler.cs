namespace EnadlaInventory.Core.Communication.ResponseMessage
{
    public abstract class ResponseMessageHandlerBase : IDisposable
    {
        private bool _isConsumed = false;

        public bool IsConsumed
        {
            get => _isConsumed;
        }

        public abstract bool WasLocallyGenerated
        {
            get;
        }
        
        
        public void Consume()
        {
            _isConsumed = true;
        }

        public abstract Task<T> ReadAsAsync<T>(int contentIndex) where T : class;

        public abstract Task<Stream> ReadAsStreamAsync(int contentIndex);

        public abstract Task<Dictionary<string, IEnumerable<string>>> GetContentHeadersAsync(int contentIndex);

        public abstract void Dispose();
    }
}
