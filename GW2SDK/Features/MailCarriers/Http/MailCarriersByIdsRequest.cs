using System.Collections.Generic;
using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.MailCarriers.Http;

[PublicAPI]
public sealed class MailCarriersByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mailcarriers")
    {
        AcceptEncoding = "gzip"
    };

    public MailCarriersByIdsRequest(IReadOnlyCollection<int> mailCarrierIds, Language? language)
    {
        Check.Collection(mailCarrierIds, nameof(mailCarrierIds));
        MailCarrierIds = mailCarrierIds;
        Language = language;
    }

    public IReadOnlyCollection<int> MailCarrierIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MailCarriersByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.MailCarrierIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}