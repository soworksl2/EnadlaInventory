using EnadlaInventory.Core.Validation;
using FluentValidation;

namespace EnadlaInventory.Core.Tests.Validation
{
    internal class PersonModelSampleValidator : SimpleFluentValidator<PersonModelSample>
    {
        #region ALL RULESETS

        public readonly static string RS_NAMES = nameof(RS_NAMES);
        public readonly static string RS_AUTH = nameof(RS_AUTH);

        #endregion

        private static PersonModelSampleValidator _instance = new PersonModelSampleValidator();

        public static PersonModelSampleValidator Instance => _instance;

        private PersonModelSampleValidator()
        {
            RuleFor(x => x.UID)
                .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE);

            RuleFor(x => x.ForceInvalid)
                .Must(o => !o).WithErrorCode(ErrorCodes.INVALID_VALUE);

            RuleSet(RS_NAMES, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE);

                RuleFor(x => x.LastName)
                    .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE);
            });

            RuleSet(RS_AUTH, () =>
            {
                RuleFor(x => x.Email)
                    .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE);

                RuleFor(x => x.Password)
                    .NotEmpty().WithErrorCode(ErrorCodes.EMPY_VALUE);
            });
        }
    }
}
