using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Extensions;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetColorsByIdRequest : HttpRequestMessage
    {
        public GetColorsByIdRequest([NotNull] IReadOnlyList<int> colorIds)
            : base(HttpMethod.Get, GetResource(colorIds))
        {
        }

        public static string GetResource([NotNull] IReadOnlyList<int> colorIds)
        {
            if (colorIds == null) throw new ArgumentNullException(nameof(colorIds));
            return $"/v2/colors?ids={colorIds.ToCsv(false)}";
        }
    }
}
