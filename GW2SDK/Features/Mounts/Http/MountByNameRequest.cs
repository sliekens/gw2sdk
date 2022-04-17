using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http;

[PublicAPI]
public sealed class MountByNameRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/types")
    {
        AcceptEncoding = "gzip"
    };

    public MountByNameRequest(MountName mountName, Language? language)
    {
        Check.Constant(mountName, nameof(mountName));
        MountName = mountName;
        Language = language;
    }

    public MountName MountName { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MountByNameRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", MountNameFormatter.FormatMountName(r.MountName));
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}
