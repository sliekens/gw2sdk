using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Professions.Http;

[PublicAPI]
public sealed class ProfessionsByNamesRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/professions")
    {
        AcceptEncoding = "gzip"
    };

    public ProfessionsByNamesRequest(IReadOnlyCollection<ProfessionName> professionNames, Language? language)
    {
        Check.Collection(professionNames, nameof(professionNames));
        ProfessionNames = professionNames;
        Language = language;
    }

    public IReadOnlyCollection<ProfessionName> ProfessionNames { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(ProfessionsByNamesRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.ProfessionNames.Select(name => name.ToString()));
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
