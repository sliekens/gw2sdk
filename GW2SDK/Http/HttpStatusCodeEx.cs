#if !NET
using System.Net;

namespace GuildWars2.Http;

[PublicAPI]
public static class HttpStatusCodeEx
{
    public const HttpStatusCode TooManyRequests = (HttpStatusCode)429;
}

#endif
