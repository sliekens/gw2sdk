using System.Text.Json;

using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Authorization;

/// <summary>Provides query methods for access token introspection and subtoken creation.</summary>
public sealed class TokenClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="TokenClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public TokenClient(HttpClient httpClient)
    {
        ThrowHelper.ThrowIfNull(httpClient);
        this.httpClient = httpClient;
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves information about the current access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public async Task<(TokenInfo Value, MessageContext Context)> GetTokenInfo(
        string accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/tokeninfo", accessToken);
        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            TokenInfo value = response.Json.RootElement.GetTokenInfo();
            return (value, response.Context);
        }
    }

    /// <summary>Creates a new access token with the specified permissions, expiration and URL restrictions. If the parent
    /// token is an API key, the subtoken will inherit the permissions of the API key. The subtoken will always have an
    /// expiration date and optionally more restricted permissions. If the parent token is also a subtoken, the new subtoken
    /// will inherit the restrictions of the old subtoken.</summary>
    /// <remarks>A subtoken expires immediately when the API key it was created from is deleted.</remarks>
    /// <param name="accessToken">An API key or subtoken. If it is a subtoken, it must have permission to use
    /// /v2/createsubtoken.</param>
    /// <param name="permissions">Optional permission restrictions. The final token permissions will be an intersection of this
    /// list and the parent token's permissions. It's possible to create a subtoken without the
    /// <see cref="Permission.Account" /> permission, but they are practically useless.</param>
    /// <param name="absoluteExpirationDate">The expiration date of the new subtoken. If none is specified, the default
    /// lifetime is 30 days. If the parent token is an API key, the lifetime cannot exceed 365 days. If the parent token is a
    /// subtoken, its lifetime cannot be exceeded.</param>
    /// <param name="allowedUrls">A list of API endpoints which may be accessed with the subtoken. For example, '/v2/account'.
    /// Case-sensitive. By default, all endpoints are allowed. If an allowlist is given, any URL not in the list will be
    /// forbidden. Only full path matches are allowed. For example, '/v2/account' does not allow access to
    /// '/v2/account/wallet'. Wildcards are also not supported. For example, you can't allow wildcard access to
    /// '/v2/characters/:id'.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    [Obsolete("Use the overload with Action<SubtokenOptionsBuilder> configureCallback instead.")]
    public Task<(CreatedSubtoken Value, MessageContext Context)> CreateSubtoken(
        string accessToken,
        IEnumerable<Permission>? permissions = null,
        DateTimeOffset? absoluteExpirationDate = null,
        IEnumerable<string>? allowedUrls = null,
        MissingMemberBehavior missingMemberBehavior = default,
#pragma warning disable RCS1231
        CancellationToken cancellationToken = default
#pragma warning restore RCS1231
    )
    {
        SubtokenOptionsBuilder optionsBuilder = new();
        if (permissions is not null)
        {
            optionsBuilder.WithPermissions(permissions);
        }

        if (absoluteExpirationDate.HasValue)
        {
            optionsBuilder.WithAbsoluteExpiration(absoluteExpirationDate.Value);
        }

        if (allowedUrls is not null)
        {
            optionsBuilder.WithAllowedUrlStrings(allowedUrls);
        }

        return CreateSubtokenCore(
            accessToken,
            optionsBuilder,
            missingMemberBehavior,
            cancellationToken
        );
    }

    /// <summary>Creates a new access token with the specified permissions, expiration and URL restrictions. If the parent
    /// token is an API key, the subtoken will inherit the permissions of the API key. The subtoken will always have an
    /// expiration date and optionally more restricted permissions. If the parent token is also a subtoken, the new subtoken
    /// will inherit the restrictions of the old subtoken.</summary>
    /// <remarks>A subtoken expires immediately when the API key it was created from is deleted.</remarks>
    /// <param name="accessToken">An API key or subtoken. If it is a subtoken, it must have permission to use
    /// /v2/createsubtoken.</param>
    /// <param name="permissions">Optional permission restrictions. The final token permissions will be an intersection of this
    /// list and the parent token's permissions. It's possible to create a subtoken without the
    /// <see cref="Permission.Account" /> permission, but they are practically useless.</param>
    /// <param name="absoluteExpirationDate">The expiration date of the new subtoken. If none is specified, the default
    /// lifetime is 30 days. If the parent token is an API key, the lifetime cannot exceed 365 days. If the parent token is a
    /// subtoken, its lifetime cannot be exceeded.</param>
    /// <param name="allowedUrls">A list of API endpoints which may be accessed with the subtoken. For example, '/v2/account'.
    /// Case-sensitive. By default, all endpoints are allowed. If an allowlist is given, any URL not in the list will be
    /// forbidden. Only full path matches are allowed. For example, '/v2/account' does not allow access to
    /// '/v2/account/wallet'. Wildcards are also not supported. For example, you can't allow wildcard access to
    /// '/v2/characters/:id'.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    [Obsolete("Use the overload with Action<SubtokenOptionsBuilder> configureCallback instead.")]
    public Task<(CreatedSubtoken Value, MessageContext Context)> CreateSubtoken(
        string accessToken,
        IEnumerable<Permission>? permissions = null,
        DateTimeOffset? absoluteExpirationDate = null,
        IEnumerable<Uri>? allowedUrls = null,
        MissingMemberBehavior missingMemberBehavior = default,
#pragma warning disable RCS1231
        CancellationToken cancellationToken = default
#pragma warning restore RCS1231
    )
    {
        SubtokenOptionsBuilder optionsBuilder = new();
        if (permissions is not null)
        {
            optionsBuilder.WithPermissions(permissions);
        }

        if (absoluteExpirationDate.HasValue)
        {
            optionsBuilder.WithAbsoluteExpiration(absoluteExpirationDate.Value);
        }

        if (allowedUrls is not null)
        {
            optionsBuilder.WithAllowedUrls(allowedUrls);
        }

        return CreateSubtokenCore(
            accessToken,
            optionsBuilder,
            missingMemberBehavior,
            cancellationToken
        );
    }

    /// <summary>Creates a new access token with the specified permissions, expiration and URL restrictions. If the parent
    /// token is an API key, the subtoken will inherit the permissions of the API key. The subtoken will always have an
    /// expiration date and optionally more restricted permissions. If the parent token is also a subtoken, the new subtoken
    /// will inherit the restrictions of the old subtoken.</summary>
    /// <remarks>A subtoken expires immediately when the API key it was created from is deleted.</remarks>
    /// <param name="accessToken">An API key or subtoken. If it is a subtoken, it must have permission to use
    /// /v2/createsubtoken.</param>
    /// <param name="configureCallback">Configures the options used during subtoken creation.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(CreatedSubtoken Value, MessageContext Context)> CreateSubtoken(
        string accessToken,
        Action<SubtokenOptionsBuilder> configureCallback,
        MissingMemberBehavior missingMemberBehavior = default,
#pragma warning disable RCS1231
        CancellationToken cancellationToken = default
#pragma warning restore RCS1231
    )
    {
        ThrowHelper.ThrowIfNull(configureCallback);

        SubtokenOptionsBuilder optionsBuilder = new();
        configureCallback(optionsBuilder);

        return CreateSubtokenCore(
            accessToken,
            optionsBuilder,
            missingMemberBehavior,
            cancellationToken
        );
    }

    private async Task<(CreatedSubtoken Value, MessageContext Context)> CreateSubtokenCore(
        string accessToken,
        SubtokenOptionsBuilder optionsBuilder,
        MissingMemberBehavior missingMemberBehavior,
        CancellationToken cancellationToken
    )
    {
        RequestBuilder requestBuilder = RequestBuilder.HttpGet("v2/createsubtoken", accessToken);
        IReadOnlyList<Permission> permissions = optionsBuilder.Permissions;
        if (permissions.Count != 0)
        {
#pragma warning disable CA1308 // Normalize strings to uppercase
            string permissionList = string.Join(",", permissions).ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
            requestBuilder.Query.Add("permissions", permissionList);
        }

        DateTimeOffset? absoluteExpirationDate = optionsBuilder.AbsoluteExpirationDate;
        if (absoluteExpirationDate.HasValue)
        {
            requestBuilder.Query.Add(
                "expire",
                absoluteExpirationDate.Value.ToUniversalTime().ToString("u")
            );
        }

        IReadOnlyList<Uri> allowedUrls = optionsBuilder.AllowedUrls;
        if (allowedUrls.Count != 0)
        {
            string urls = string.Join(",", allowedUrls.Select(uri => uri.ToString()));
            requestBuilder.Query.Add("urls", urls);
        }

        using HttpRequestMessage request = requestBuilder.Build();
        (JsonDocument Json, MessageContext Context) response = await httpClient.AcceptJsonAsync(request, cancellationToken)
            .ConfigureAwait(false);
        using (response.Json)
        {
            JsonOptions.MissingMemberBehavior = missingMemberBehavior;
            CreatedSubtoken value = response.Json.RootElement.GetCreatedSubtoken();
            return (value, response.Context);
        }
    }
}
