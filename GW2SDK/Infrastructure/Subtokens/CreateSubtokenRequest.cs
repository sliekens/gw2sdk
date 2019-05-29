using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;

namespace GW2SDK.Infrastructure.Subtokens
{
    public sealed class CreateSubtokenRequest : HttpRequestMessage
    {
        private CreateSubtokenRequest([NotNull] Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            [CanBeNull]
            private readonly IReadOnlyList<Permission> _permissions;

            public Builder([CanBeNull] string accessToken = null, [CanBeNull] IReadOnlyList<Permission> permissions = null)
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    AccessToken = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                _permissions = permissions;
            }

            public AuthenticationHeaderValue AccessToken { get; }

            public CreateSubtokenRequest GetRequest()
            {
                var resource = "/v2/createsubtoken";
                if (_permissions is object && _permissions.Count != default)
                {
                    resource += $"?permissions={_permissions.ToCsv(false).ToLowerInvariant()}";
                }

                return new CreateSubtokenRequest(new Uri(resource, UriKind.Relative)) { Headers = { Authorization = AccessToken } };
            }
        }
    }
}
