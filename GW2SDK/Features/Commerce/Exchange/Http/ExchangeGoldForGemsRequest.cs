using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Exchange.Http;

[PublicAPI]
public sealed class ExchangeGoldForGemsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/exchange/coins");

    public ExchangeGoldForGemsRequest(int coinsCount)
    {
        CoinsCount = coinsCount;
    }

    public int CoinsCount { get; }

    public static implicit operator HttpRequestMessage(ExchangeGoldForGemsRequest r)
    {
        QueryBuilder search = new();
        search.Add("quantity", r.CoinsCount);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
