using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http
{
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
            var search = new QueryBuilder();
            search.Add("id", FormatMountName(r.MountName));
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }

        private static string FormatMountName(MountName mountName)
        {
            return mountName switch
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
    }
}
