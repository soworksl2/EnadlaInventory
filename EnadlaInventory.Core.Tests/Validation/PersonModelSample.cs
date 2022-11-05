using EnadlaInventory.Core.Validation;

namespace EnadlaInventory.Core.Tests.Validation
{
    public class PersonModelSample : ObservableAndValidatableObject<PersonModelSample>
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }
}
