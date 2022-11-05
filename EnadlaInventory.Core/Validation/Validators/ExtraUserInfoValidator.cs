using EnadlaInventory.Core.Communication.Models;
using FluentValidation;

namespace EnadlaInventory.Core.Validation.Validators
{
    public class ExtraUserInfoValidator : SimpleFuentValidator<ExtraUserInfo>
    {
        public ExtraUserInfoValidator()
        {
            RuleFor(o => o.CreatorMachine)
                .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE);
        }
    }
}
