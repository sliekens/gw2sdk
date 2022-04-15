using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Http;

[PublicAPI]
public sealed class TraitByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/traits")
    {
        AcceptEncoding = "gzip"
    };

    public TraitByIdRequest(int traitId, Language? language)
    {
        TraitId = traitId;
        Language = language;
    }

    public int TraitId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(TraitByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.TraitId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
