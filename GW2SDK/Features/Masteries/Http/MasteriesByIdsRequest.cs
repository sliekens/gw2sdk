using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Masteries.Http;

[PublicAPI]
public sealed class MasteriesByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/masteries")
    {
        AcceptEncoding = "gzip"
    };

    public MasteriesByIdsRequest(IReadOnlyCollection<int> masteryIds, Language? language)
    {
        Check.Collection(masteryIds, nameof(masteryIds));
        MasteryIds = masteryIds;
        Language = language;
    }

    public IReadOnlyCollection<int> MasteryIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MasteriesByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.MasteryIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
