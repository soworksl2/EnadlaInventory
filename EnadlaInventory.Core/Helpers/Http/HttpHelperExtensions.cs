using System.Net.Http.Headers;

namespace EnadlaInventory.Core.Helpers.Http
{
    internal static class HttpHelperExtensions
    {
        static internal bool HasErrorStatusCode(this HttpResponseMessage response)
        {
            return (int)response.StatusCode >= 400;
        }

        static internal bool Is(this MediaTypeHeaderValue? mediaType, string? mimeType)
        {
            if (mediaType is null)
            {
                if (mimeType is null)
                    return true;

                return false;
            }
            else
            {
                if (mimeType is null)
                    return false;

                return mediaType.MediaType == mimeType;
            }
        }
    }
}
