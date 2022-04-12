using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Currencies.Http;

[PublicAPI]
public sealed class CurrenciesIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/currencies")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(CurrenciesIndexRequest _)
    {
        return Template.Compile();
    }
}