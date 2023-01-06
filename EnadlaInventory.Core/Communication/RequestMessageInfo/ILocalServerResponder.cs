using EnadlaInventory.Core.Communication.ResponseMessage;

namespace EnadlaInventory.Core.Communication.RequestMessageInfo
{
    public interface ILocalServerResponder
    {
        public ResponseMessageHandlerBase? TryGenerateLocalResponse(); 
    }
}
