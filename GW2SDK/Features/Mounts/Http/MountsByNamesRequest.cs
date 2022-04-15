using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http;

[PublicAPI]
public sealed class MountsByNamesRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/types")
    {
        AcceptEncoding = "gzip"
    };

    public MountsByNamesRequest(IReadOnlyCollection<MountName> mountNames, Language? language)
    {
        Check.Collection(mountNames, nameof(mountNames));
        MountNames = mountNames;
        Language = language;
    }

    public IReadOnlyCollection<MountName> MountNames { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MountsByNamesRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.MountNames.Select(name => FormatMountName(name)));
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }

    private static string FormatMountName(MountName mountName) =>
        mountName switch
        {
            MountName.Griffon => "griffon",
            MountName.Jackal => "jackal",
            MountName.Raptor => "raptor",
            MountName.RollerBeetle => "roller_beetle",
            MountName.Skimmer => "skimmer",
            MountName.Skyscale => "skyscale",
            MountName.Springer => "springer",
            MountName.Warclaw => "warclaw",
            _ => throw new NotSupportedException("Could not format mount name.")
        };
}
