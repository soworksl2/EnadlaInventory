using EnadlaInventory.Core.Communication.Models;
using FluentValidation;

namespace EnadlaInventory.Core.Validation.Validators
{
    public class ExtraUserInfoValidator : SimpleFuentValidator<ExtraUserInfo>
    {
        private static ExtraUserInfoValidator _instance = new ExtraUserInfoValidator();


        public static ExtraUserInfoValidator Instance => _instance;


        private ExtraUserInfoValidator()
        {
            RuleFor(o => o.CreatorMachine)
                .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE);
        }
    }
}
