namespace HttpUtilities
{
    public static class HttpResponseMessageExtensions
    {
        public static bool IsStatusCodeCategory(this HttpResponseMessage responseMessage, StatusCodeCategories statusCodeCategories)
        {
            return (int)responseMessage.StatusCode switch
            {
                >= 100 and < 200 => statusCodeCategories.HasFlag(StatusCodeCategories.Information),
                >= 200 and < 300 => statusCodeCategories.HasFlag(StatusCodeCategories.Successful),
                >= 300 and < 400 => statusCodeCategories.HasFlag(StatusCodeCategories.Redirection),
                >= 400 and < 500 => statusCodeCategories.HasFlag(StatusCodeCategories.Error),
                >= 500 and < 600 => statusCodeCategories.HasFlag(StatusCodeCategories.ServerError),
                _ => false
            };
        }
    }
}
