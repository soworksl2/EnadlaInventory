using FluentValidation;
using EnadlaInventory.Core.Communication.Models;

namespace EnadlaInventory.Core.Validation.Validators
{
    public class UserInfoValidator : SimpleFuentValidator<UserInfo>
    {
        #region All RuleSets

        public static readonly string RS_AUTH = "AUTH";
        public static readonly string RS_SIGNUP_EXTRAS = "SIGNUP_EXTRAS";

        #endregion

        private const int MIN_PASSWORD_LENGHT = 8;
        private const int MIN_OWNER_NAME_LENGHT = 3;

        public UserInfoValidator()
        {
            RuleSet(RS_AUTH, () =>
            {
                RuleFor(o => o.Email)
                    .NotEmpty().WithErrorCode(ErrorCodes.EMPTY_EMAIL)
                    .EmailAddress().WithErrorCode(ErrorCodes.INVALID_EMAIL);

                RuleFor(o => o.Password)
                    .NotEmpty().WithErrorCode(ErrorCodes.EMPTY_PASSWORD)
                    .MinimumLength(MIN_PASSWORD_LENGHT).WithErrorCode(ErrorCodes.SHORT_PASSWORD);
            });

            RuleSet(RS_SIGNUP_EXTRAS, () =>
            {
                RuleFor(o => o.ConfirmPassword)
                    .Equal(o => o.Password).WithErrorCode(ErrorCodes.DIFERENT_CONFIRM_PASSWORD);

                RuleFor(o => o.OwnerName)
                    .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE)
                    .MinimumLength(MIN_OWNER_NAME_LENGHT).WithErrorCode(ErrorCodes.SHORT_VALUE);

                RuleFor(o => o.ExtraInfo)
                    .SetValidator(new ExtraUserInfoValidator()).WithErrorCode(ErrorCodes.INVALID_VALUE);
            });
        }
    }
}
