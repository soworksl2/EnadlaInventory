namespace EnadlaInventory.Core.Validation
{
    public interface ISimpleValidator<TValidableObj>
    {
        public static readonly string RS_DEFAULT = "default";

        public ValidationResult Validate(TValidableObj obj, string? propertyName = null, string[]? ruleSets = null);
    }
}
