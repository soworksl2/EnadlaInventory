using FluentValidation;

namespace EnadlaInventory.Core.Validation
{
    public abstract class SimpleFluentValidator<TTarget>: AbstractValidator<TTarget>, ISimpleValidator<TTarget>
    {
        public static readonly string RS_DEFAULT = "default";


        //TODO: validate just validate by ruleSets because FluentValidator does not bring that funcionality
        //So all properties are validated if they are inside of a specific ruleset and then will be avoided
        //at the moment of parse to EnadlaInventory ValidationResult - Jimy Aguasviva 16-november-2022
        public ValidationResult Validate(TTarget obj, string? propertyName = null, params string[]? ruleSets)
        {
            ValidationContext<TTarget> context = ValidationContext<TTarget>.CreateWithOptions(obj, x =>
            {
                if (ruleSets is not null && ruleSets.Length > 0)
                    x.IncludeRuleSets(ruleSets);
                else
                    x.IncludeAllRuleSets();
            });

            FluentValidation.Results.ValidationResult validationResult = Validate(context);
            List<ValidationFailure> castedErrors = new List<ValidationFailure>(validationResult.Errors.Count());
            foreach (var error in validationResult.Errors)
            {
                if (!string.IsNullOrEmpty(propertyName) && error.PropertyName != propertyName)
                {
                    continue;
                }

                castedErrors.Add(new ValidationFailure(error.PropertyName, error.ErrorCode));
            }

            return new ValidationResult()
            {
                ExecutedRuleSets = validationResult.RuleSetsExecuted,
                Errors = castedErrors
            };
        }
    }
}
