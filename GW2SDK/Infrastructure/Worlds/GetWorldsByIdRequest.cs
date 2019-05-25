using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetWorldsByIdRequest : HttpRequestMessage
    {
        public GetWorldsByIdRequest([NotNull] IReadOnlyList<int> worldIds)
            : base(HttpMethod.Get, GetResource(worldIds))
        {
        }

        public static string GetResource([NotNull] IReadOnlyList<int> worldIds)
        {
            if (worldIds == null) throw new ArgumentNullException(nameof(worldIds));
            return $"/v2/worlds?ids={worldIds.ToCsv(false)}";
        }
    }
}
