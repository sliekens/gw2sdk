using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http;

[PublicAPI]
public sealed class MailCarrierByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mailcarriers")
    {
        AcceptEncoding = "gzip"
    };

    public MailCarrierByIdRequest(int mailCarrierId, Language? language)
    {
        MailCarrierId = mailCarrierId;
        Language = language;
    }

    public int MailCarrierId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MailCarrierByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.MailCarrierId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}