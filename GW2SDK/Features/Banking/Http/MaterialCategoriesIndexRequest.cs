using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Banking.Http;

[PublicAPI]
public sealed class MaterialCategoriesIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/materials")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(MaterialCategoriesIndexRequest _) => Template.Compile();
}
