using System.Net;

namespace GW2SDK.Infrastructure.Common
{
    public static class HttpStatusCodeEx
    {
        public const HttpStatusCode TooManyRequests = (HttpStatusCode) 429;
    }
}
