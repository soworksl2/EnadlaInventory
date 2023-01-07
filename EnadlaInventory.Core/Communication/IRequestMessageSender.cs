namespace EnadlaInventory.Core.Communication
{
    public interface IRequestMessageSender
    {
        public Task<ResponseMessageHandlerBase> SendRequestMessageAsync(IRequestMessageInfoHandler requestMessageInfo);
    }
}
