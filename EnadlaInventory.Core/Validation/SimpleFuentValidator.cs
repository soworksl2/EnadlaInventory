using FluentValidation;

namespace EnadlaInventory.Core.Validation
{
    //TODO: this class need to be validated - Jimy Aguasviva 5-november-2022
    public abstract class SimpleFuentValidator<TTarget>: AbstractValidator<TTarget>, ISimpleValidator<TTarget>
    {
        public ValidationResult Validate(TTarget obj, string? propertyName = null, string[]? ruleSets = null)
        {
            ValidationContext<TTarget> context = ValidationContext<TTarget>.CreateWithOptions(obj, x =>
            {
                if (!string.IsNullOrEmpty(propertyName))
                    x.IncludeProperties(propertyName);

                x.IncludeRulesNotInRuleSet();

                if (ruleSets is not null)
                    x.IncludeRuleSets(ruleSets);
            });

            FluentValidation.Results.ValidationResult validationResult = Validate(context);
            List<ValidationFailure> castedErrors = new List<ValidationFailure>(validationResult.Errors.Count());
            foreach (var error in validationResult.Errors)
                castedErrors.Add(new ValidationFailure(error.PropertyName, error.ErrorCode));

            return new ValidationResult()
            {
                ExecutedRuleSets = validationResult.RuleSetsExecuted,
                Errors = castedErrors
            };
        }
    }
}
