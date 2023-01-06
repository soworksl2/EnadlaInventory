using EnadlaInventory.Core.Communication.Models;
using EnadlaInventory.Core.Validation.Validators;
using EnadlaInventory.Core.Validation;

namespace EnadlaInventory.Core.Tests.Validation.Validator
{
    [TestClass]
    public class TestExtraUserInfoValidator
    {
        private ExtraUserInfo GetGoodExtraUserInfo()
        {
            return new ExtraUserInfo()
            {
                CreationDate = DateTime.Now,
                UID = "someUID",
                CreatorMachine = "someCreatorMahine"
            };
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void Validate_with_CreatorMachine_as_empty_is_invalid(string? creatorMachine)
        {
            ExtraUserInfoValidator validator = ExtraUserInfoValidator.Instance;
            ExtraUserInfo extraInfo = GetGoodExtraUserInfo();
            extraInfo.CreatorMachine = creatorMachine;

            ValidationResult result = validator.Validate(extraInfo);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Where(x => x.PropertyName == nameof(ExtraUserInfo.CreatorMachine) && x.ErrorCode == ErrorCodes.EMPY_VALUE).Count() >= 1);
        }
    }
}
