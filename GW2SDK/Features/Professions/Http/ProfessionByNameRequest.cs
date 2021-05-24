using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionByNameRequest
    {
        public ProfessionByNameRequest(ProfessionName professionName)
        {
            if (!Enum.IsDefined(professionName))
            {
                throw new ArgumentException("Profession name must be defined.", nameof(professionName));
            }

            ProfessionName = professionName;
        }

        public ProfessionName ProfessionName { get; }

        public static implicit operator HttpRequestMessage(ProfessionByNameRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ProfessionName.ToString());
            var location = new Uri($"/v2/professions?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
