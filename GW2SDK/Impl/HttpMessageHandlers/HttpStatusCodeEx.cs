using System.Net;

namespace GW2SDK.Impl.HttpMessageHandlers
{
    public static class HttpStatusCodeEx
    {
        public const HttpStatusCode TooManyRequests = (HttpStatusCode) 429;
    }
}
