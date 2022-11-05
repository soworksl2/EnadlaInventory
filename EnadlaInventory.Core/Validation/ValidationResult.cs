namespace EnadlaInventory.Core.Validation
{
    public class ValidationResult
    {
        private List<ValidationFailure> _errors = new List<ValidationFailure>();

        public bool IsValid => Errors.Count <= 0;
        public List<ValidationFailure> Errors
        {
            get => _errors;
            set
            {
                _errors = value is null? new List<ValidationFailure>() : value;
            }
        }
        public string[] ExecutedRuleSets { get; set; }
    }
}
