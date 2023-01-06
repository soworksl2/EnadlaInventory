using EnadlaInventory.Core.Communication.Models;

namespace EnadlaInventory.Core.Tests.Utilities
{
    internal static class ModelFactory
    {
        static internal UserInfo CreateValidUserInfo()
        {
            return new UserInfo()
            {
                UID = "someId",
                Email = "jimy.waner11@gmail.com",
                Password = "123456789",
                ConfirmPassword = "123456789",
                OwnerName = "Jimy",
                ExtraInfo = new ExtraUserInfo()
                {
                    UID = "someId",
                    CreationDate = DateTime.Now,
                    CreatorMachine = "someIdMachine"
                }
            };
        }
    }
}
