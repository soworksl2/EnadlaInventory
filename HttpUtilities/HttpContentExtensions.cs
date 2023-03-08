using System.Net.Http.Headers;
using System.Text.Json.Nodes;

namespace HttpUtilities
{
    public static class HttpContentExtensions
    {
        public static JsonNode ReadAsJson(this HttpContent content)
        {
            if (!content.IsContentType(MediaTypes.Application_Json))
            {
                throw new Exception($"the content type is not '{MediaTypes.Application_Json}'");
            }

            JsonNode? output = JsonNode.Parse(content.ReadAsStream());

            if (output is null)
            {
                throw new Exception("something went wrong parsing the jsonContent");
            }

            return output;
        }

        public static bool IsContentType(this HttpContent content, string? mediaType)
        {
            MediaTypeHeaderValue? contentType = content.Headers.ContentType;

            if (contentType is null)
            {
                return mediaType is null;
            }

            return contentType.MediaType == mediaType;
        }
    }
}
