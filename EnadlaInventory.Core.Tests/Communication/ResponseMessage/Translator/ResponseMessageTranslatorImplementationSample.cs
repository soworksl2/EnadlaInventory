using EnadlaInventory.Core.Communication.ResponseMessage;
using EnadlaInventory.Core.Communication.ResponseMessage.Translators;

namespace EnadlaInventory.Core.Tests.Communication.ResponseMessage.Translator
{
    internal class ResponseMessageTranslatorImplementationSample : ResponseMessageTranslatorBase
    {
        public ResponseMessageTranslatorImplementationSample(ResponseMessageHandlerBase responseMessageHandler) : 
            base(responseMessageHandler)
        {

        }
    }
}
