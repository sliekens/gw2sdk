using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http
{
    [PublicAPI]
    public sealed class ColorByIdRequest
    {
        public ColorByIdRequest(int colorId, Language? language)
        {
            ColorId = colorId;
            Language = language;
        }

        public int ColorId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ColorByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ColorId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/colors?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
