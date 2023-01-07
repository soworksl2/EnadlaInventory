namespace EnadlaInventory.Core.Communication
{
    public interface ILocalServerResponder
    {
        public ResponseMessageHandlerBase? TryGenerateLocalResponse();
    }
}
