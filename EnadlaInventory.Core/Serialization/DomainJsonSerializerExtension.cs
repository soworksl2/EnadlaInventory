using System.Text.Json;
using System.Text.Json.Nodes;

namespace EnadlaInventory.Core.Serialization
{
    internal static class DomainJsonSerializerExtension
    {
        internal static T DeserializeWithDomainOptions<T>(this JsonNode jsonNode)
        {
            T? output = jsonNode.Deserialize<T>();

            if (output is null)
            {
                throw new Exception("Something went wrong deserializing");
            }

            return output;
        }
    }
}
