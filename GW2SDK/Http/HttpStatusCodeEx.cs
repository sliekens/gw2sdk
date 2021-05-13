using System.Net;
using GW2SDK.Annotations;

namespace GW2SDK.Http
{
    [PublicAPI]
    public static class HttpStatusCodeEx
    {
        public const HttpStatusCode TooManyRequests = (HttpStatusCode) 429;
    }
}
