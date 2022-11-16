using EnadlaInventory.Core.Validation;

namespace EnadlaInventory.Core.Tests.Validation
{
    public class PersonModelSample : ObservableAndValidatableObject<PersonModelSample>
    {
        public string? UID { get; set; }
        public bool ForceInvalid { get; set; }

        public string? Name { get; set; }
        public string? LastName { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
