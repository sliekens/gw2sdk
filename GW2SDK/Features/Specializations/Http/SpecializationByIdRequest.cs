using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Specializations.Http;

[PublicAPI]
public sealed class SpecializationByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/specializations")
    {
        AcceptEncoding = "gzip"
    };

    public SpecializationByIdRequest(int specializationId, Language? language)
    {
        SpecializationId = specializationId;
        Language = language;
    }

    public int SpecializationId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(SpecializationByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.SpecializationId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
