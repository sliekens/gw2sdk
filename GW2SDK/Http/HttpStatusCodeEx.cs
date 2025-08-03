namespace GuildWars2.Http;

/// <summary>Provides additional HTTP status codes which are missing in older .NET versions.</summary>
#if NET
[Obsolete("Use System.Net.HttpStatusCode instead.")]
#endif
[PublicAPI]
public static class HttpStatusCodeEx
{
    /// <summary>Equivalent to HTTP status 429. TooManyRequests indicates that the user has sent too many requests in a given
    /// amount of time.</summary>
#if NET
    public const HttpStatusCode TooManyRequests = HttpStatusCode.TooManyRequests;
#else
    public const HttpStatusCode TooManyRequests = (HttpStatusCode)429;
#endif
}
