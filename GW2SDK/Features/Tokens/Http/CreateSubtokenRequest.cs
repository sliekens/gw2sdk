using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Tokens.Http;

[PublicAPI]
public sealed class CreateSubtokenRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/createsubtoken");

    public CreateSubtokenRequest(
        string? accessToken,
        IReadOnlyCollection<Permission>? permissions = null,
        DateTimeOffset? absoluteExpirationDate = null,
        IReadOnlyCollection<string>? urls = null
    )
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
        QueryBuilder args = new();
        if (r.Permissions is { Count: not 0 })
        {
            args.Add("permissions",
                string.Join(",", r.Permissions)
                    .ToLowerInvariant());
        }

        if (r.AbsoluteExpirationDate.HasValue)
        {
            args.Add("expire",
                r.AbsoluteExpirationDate.Value.ToUniversalTime()
                    .ToString("s"));
        }

        if (r.Urls is { Count: not 0 })
        {
            args.Add("urls", string.Join(",", r.Urls.Select(Uri.EscapeDataString)));
        }

        var request = Template with
        {
            BearerToken = r.AccessToken,
            Arguments = args
        };

        return request.Compile();
    }
}
