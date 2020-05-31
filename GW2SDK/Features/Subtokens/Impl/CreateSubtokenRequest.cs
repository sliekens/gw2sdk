using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using GW2SDK.Enums;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Subtokens.Impl
{
    public sealed class CreateSubtokenRequest
    {
        public CreateSubtokenRequest(
            string? accessToken = null,
            IReadOnlyCollection<Permission>? permissions = null,
            DateTimeOffset? absoluteExpirationDate = null,
            IReadOnlyCollection<string>? urls = null)
        {
            AccessToken = accessToken;
            Permissions = permissions;
            AbsoluteExpirationDate = absoluteExpirationDate;
            Urls = urls;
        }

        public DateTimeOffset? AbsoluteExpirationDate { get; }

        public IReadOnlyCollection<Permission>? Permissions { get; }

        public IReadOnlyCollection<string>? Urls { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(CreateSubtokenRequest r)
        {
            var args = new QueryBuilder();
            if (r.Permissions is object && r.Permissions.Count != 0)
            {
                args.Add("permissions", string.Join(',', r.Permissions).ToLowerInvariant());
            }

            if (r.AbsoluteExpirationDate.HasValue)
            {
                args.Add("expire", r.AbsoluteExpirationDate.Value.ToUniversalTime().ToString("s"));
            }

            if (r.Urls is object && r.Urls.Count != 0)
            {
                args.Add("urls", string.Join(',', r.Urls.Select(Uri.EscapeDataString)));
            }

            var uriString = "/v2/createsubtoken";
            if (args.Count != 0)
            {
                uriString += $"?{args}";
            }

            var location = new Uri(uriString, UriKind.Relative);
            return new HttpRequestMessage(Get, location)
            {
                Headers =
                {
                    Authorization = string.IsNullOrWhiteSpace(r.AccessToken)
                        ? default
                        : new AuthenticationHeaderValue("Bearer", r.AccessToken)
                }
            };
        }
    }
}
