﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Enums;
using GW2SDK.Extensions;

namespace GW2SDK.Subtokens.Impl
{
    public sealed class CreateSubtokenRequest : HttpRequestMessage
    {
        private CreateSubtokenRequest(Uri requestUri)
            : base(HttpMethod.Get, requestUri)
        {
        }

        public sealed class Builder
        {
            private readonly DateTimeOffset? _absoluteExpirationDate;

            private readonly IReadOnlyCollection<Permission>? _permissions;

            private readonly IReadOnlyCollection<string>? _urls;

            public Builder(
                string? accessToken = null,
                IReadOnlyCollection<Permission>? permissions = null,
                DateTimeOffset? absoluteExpirationDate = null,
                IReadOnlyCollection<string>? urls = null)
            {
                if (!string.IsNullOrEmpty(accessToken))
                {
                    AccessToken = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                _permissions = permissions;
                _absoluteExpirationDate = absoluteExpirationDate;
                _urls = urls;
            }

            public AuthenticationHeaderValue? AccessToken { get; }

            public CreateSubtokenRequest GetRequest()
            {
                var resource = "/v2/createsubtoken";

                var arguments = new List<string>();
                if (_permissions is object && _permissions.Count != default)
                {
                    arguments.Add($"permissions={_permissions.ToCsv().ToLowerInvariant()}");
                }

                if (_absoluteExpirationDate.HasValue)
                {
                    arguments.Add($"expire={_absoluteExpirationDate.Value.ToUniversalTime():s}");
                }

                if (_urls is object && _urls.Count != default)
                {
                    arguments.Add($"urls={_urls.Select(Uri.EscapeDataString).ToCsv()}");
                }

                if (arguments.Count != 0)
                {
                    resource += "?" + string.Join("&", arguments);
                }

                return new CreateSubtokenRequest(new Uri(resource, UriKind.Relative)) { Headers = { Authorization = AccessToken } };
            }
        }
    }
}
