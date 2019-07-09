﻿using System;
using System.Net.Http;
using GW2SDK.Annotations;

namespace GW2SDK.Colors.Impl
{
    public sealed class GetColorByIdRequest : HttpRequestMessage
    {
        private GetColorByIdRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly int _colorId;

            public Builder(int colorId)
            {
                _colorId = colorId;
            }

            public GetColorByIdRequest GetRequest()
            {
                var resource = new Uri($"/v2/colors?id={_colorId}", UriKind.Relative);
                return new GetColorByIdRequest(resource);
            }
        }
    }
}
