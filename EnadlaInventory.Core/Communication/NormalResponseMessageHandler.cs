using EnadlaInventory.Core.Helpers.Http;
using EnadlaInventory.Core.Serialization;
using System.Net.Http.Json;

namespace EnadlaInventory.Core.Communication
{
    public sealed class NormalResponseMessageHandler : ResponseMessageHandlerBase
    {
        private HttpResponseMessage _rawResponse;
        private MultipartMemoryStreamProvider? _multipartProvider = null;


        public HttpResponseMessage RawResponse
        {
            get => _rawResponse;
        }


        public override bool WasLocallyGenerated
        {
            get => false;
        }


        public NormalResponseMessageHandler(HttpResponseMessage rawResponse)
        {
            _rawResponse = rawResponse;
        }


        public override void Dispose()
        {
            _rawResponse.Dispose();
        }

        public override async Task<T> ReadAsAsync<T>(int contentIndex)
            where T : class
        {
            HttpContent specifiedContent = await GetSpecificContentAsync(contentIndex);

            T? output = await specifiedContent.ReadFromJsonAsync<T>(JsonOptionsHandler.DomainJsonOptions);

            if (output is null)
            {
                throw new InvalidOperationException("the message could not be casted");
            }

            return output;
        }

        public override async Task<Stream> ReadAsStreamAsync(int contentIndex)
        {
            HttpContent specifiedContent = await GetSpecificContentAsync(contentIndex);

            return await specifiedContent.ReadAsStreamAsync();
        }

        public override async Task<Dictionary<string, IEnumerable<string>>> GetContentHeadersAsync(int contentIndex)
        {
            HttpContent specifiedContent = await GetSpecificContentAsync(contentIndex);

            return specifiedContent.Headers.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private async Task<HttpContent> GetSpecificContentAsync(int contentIndex)
        {
            if (!_rawResponse.Content.Headers.ContentType.Is(MimesTypes.Multipart_FormData))
            {
                return GetSpecificContentFromNonMultipart(contentIndex);
            }
            else
            {
                return await GetSpecificContentFromMultipartAsync(contentIndex);
            }
        }

        private HttpContent GetSpecificContentFromNonMultipart(int contentIndex)
        {
            if (contentIndex != 0)
            {
                throw new OverflowException($"the contentIndex {contentIndex} does not exists in the message");
            }

            return _rawResponse.Content;
        }

        private async Task<HttpContent> GetSpecificContentFromMultipartAsync(int contentIndex)
        {
            if (_multipartProvider is null)
            {
                _multipartProvider = await _rawResponse.Content.ReadAsMultipartAsync();
            }

            if (contentIndex > _multipartProvider.Contents.Count() || contentIndex < 0)
            {
                throw new OverflowException($"the contentIndex {contentIndex} does not exists in the message");
            }

            return _multipartProvider.Contents[contentIndex];
        }
    }
}
