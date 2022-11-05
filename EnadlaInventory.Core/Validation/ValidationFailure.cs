namespace EnadlaInventory.Core.Validation
{
    public class ValidationFailure
    {
        public string PropertyName { get; set; }
        public string ErrorCode { get; set; }

        public ValidationFailure(string propertyName, string errorCode)
        {
            PropertyName = propertyName;
            ErrorCode = errorCode;
        }
    }
}
