using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnadlaInventory.Core.Communication
{
    public interface IRequestMessageSender
    {
        public Task<ResponseMessageHandlerBase> SendRequestMessageAsync(IRequestMessageInfoHandler requestMessageInfo);
    }
}
