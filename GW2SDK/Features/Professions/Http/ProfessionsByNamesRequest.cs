using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionsByNamesRequest
    {
        public ProfessionsByNamesRequest(IReadOnlyCollection<ProfessionName> professionNames, Language? language)
        {
            if (professionNames is null)
            {
                throw new ArgumentNullException(nameof(professionNames));
            }

            if (professionNames.Count == 0)
            {
                throw new ArgumentException("Profession names cannot be an empty collection.", nameof(professionNames));
            }

            if (professionNames.Any(name => !Enum.IsDefined(name)))
            {
                throw new ArgumentException("All profession names must be defined.", nameof(professionNames));
            }

            ProfessionNames = professionNames;
            Language = language;
        }

        public IReadOnlyCollection<ProfessionName> ProfessionNames { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ProfessionsByNamesRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ProfessionNames.Select(name => name.ToString()));
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/professions?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
