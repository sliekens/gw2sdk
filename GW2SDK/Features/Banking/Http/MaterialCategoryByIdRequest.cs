using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Banking.Http;

[PublicAPI]
public sealed class MaterialCategoryByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/materials")
    {
        AcceptEncoding = "gzip"
    };

    public MaterialCategoryByIdRequest(int materialCategoryId, Language? language)
    {
        MaterialCategoryId = materialCategoryId;
        Language = language;
    }

    public int MaterialCategoryId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MaterialCategoryByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.MaterialCategoryId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}