using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountsByNamesRequest
    {
        public MountsByNamesRequest(IReadOnlyCollection<MountName> mountNames, Language? language)
        {
            if (mountNames is null)
            {
                throw new ArgumentNullException(nameof(mountNames));
            }

            if (mountNames.Count == 0)
            {
                throw new ArgumentException("Mount names cannot be an empty collection.", nameof(mountNames));
            }

            if (mountNames.Any(name => !Enum.IsDefined(typeof(MountName), name)))
            {
                throw new ArgumentException("All mount names must be defined.", nameof(mountNames));
            }

            MountNames = mountNames;
            Language = language;
        }

        public IReadOnlyCollection<MountName> MountNames { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MountsByNamesRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.MountNames.Select(name => FormatMountName(name)));
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
