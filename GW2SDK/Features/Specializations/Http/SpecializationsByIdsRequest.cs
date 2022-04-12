using System.Collections.Generic;
using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Specializations.Http;

[PublicAPI]
public sealed class SpecializationsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/specializations")
    {
        AcceptEncoding = "gzip"
    };

    public SpecializationsByIdsRequest(IReadOnlyCollection<int> specializationIds, Language? language)
    {
        Check.Collection(specializationIds, nameof(specializationIds));
        SpecializationIds = specializationIds;
        Language = language;
    }

    public IReadOnlyCollection<int> SpecializationIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(SpecializationsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.SpecializationIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}