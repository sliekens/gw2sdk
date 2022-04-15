using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Http;

[PublicAPI]
public sealed class CurrencyByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/currencies")
    {
        AcceptEncoding = "gzip"
    };

    public CurrencyByIdRequest(int currencyId, Language? language)
    {
        CurrencyId = currencyId;
        Language = language;
    }

    public int CurrencyId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(CurrencyByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.CurrencyId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
