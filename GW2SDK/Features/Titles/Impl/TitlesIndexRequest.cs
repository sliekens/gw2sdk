using System;
using System.Net.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Titles.Impl
{
    public sealed class TitlesIndexRequest
    {
        public static implicit operator HttpRequestMessage(TitlesIndexRequest _)
        {
            var location = new Uri("/v2/titles", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
