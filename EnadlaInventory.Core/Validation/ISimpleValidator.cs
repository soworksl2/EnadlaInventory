namespace EnadlaInventory.Core.Validation
{
    public interface ISimpleValidator<TValidableObj>
    {
        public ValidationResult Validate(TValidableObj obj, string? propertyName = null, string[]? ruleSets = null);
    }
}
