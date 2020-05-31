using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorsByIdsRequest
    {
        public ColorsByIdsRequest(IReadOnlyCollection<int> colorIds)
        {
            if (colorIds == null)
            {
                throw new ArgumentNullException(nameof(colorIds));
            }

            if (colorIds.Count == 0)
            {
                throw new ArgumentException("Color IDs cannot be an empty collection.", nameof(colorIds));
            }

            ColorIds = colorIds;
        }

        public IReadOnlyCollection<int> ColorIds { get; }

        public static implicit operator HttpRequestMessage(ColorsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ColorIds);
            var location = new Uri($"/v2/colors?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
