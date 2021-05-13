using System;
using System.Net.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http
{
    [PublicAPI]
    public sealed class ColorByIdRequest
    {
        public ColorByIdRequest(int colorId)
        {
            ColorId = colorId;
        }

        public int ColorId { get; }

        public static implicit operator HttpRequestMessage(ColorByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ColorId);
            var location = new Uri($"/v2/colors?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
