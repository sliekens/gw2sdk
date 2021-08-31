using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountByNameRequest
    {
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
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/mounts/types?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
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
