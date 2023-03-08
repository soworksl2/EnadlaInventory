namespace HttpUtilities
{
    [Flags]
    public enum StatusCodeCategories : byte
    {
        Information = 1,
        Successful = 2,
        Redirection = 4,
        Error = 8,
        ServerError = 16
    }
}
