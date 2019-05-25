using System.Net.Http;

namespace GW2SDK.Infrastructure.Subtokens
{
    public sealed class CreateSubtokenRequest : HttpRequestMessage
    {
        public CreateSubtokenRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        private static string Resource => "/v2/createsubtoken";
    }
}