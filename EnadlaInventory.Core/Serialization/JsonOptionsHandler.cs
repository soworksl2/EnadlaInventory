using System.Text.Json;

namespace EnadlaInventory.Core.Serialization
{
    internal static class JsonOptionsHandler
    {
        private static JsonSerializerOptions? _domainJsonOptions;

        internal static JsonSerializerOptions DomainJsonOptions
        {
            get
            {
                if (_domainJsonOptions is null)
                {
                    _domainJsonOptions = new JsonSerializerOptions();
                    _domainJsonOptions.Converters.Add(new DomainDateTimeJsonConverter());
                }

                return _domainJsonOptions;
            }
        }
    }
}
