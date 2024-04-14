using GuildWars2.Authorization.Http;

namespace GuildWars2.Authorization;

/// <summary>Provides query methods for access token introspection and subtoken creation.</summary>
[PublicAPI]
public sealed class TokenClient
{
    private readonly HttpClient httpClient;

    /// <summary>Initializes a new instance of the <see cref="TokenClient" /> class.</summary>
    /// <param name="httpClient">The HTTP client used for making API requests.</param>
    public TokenClient(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        httpClient.BaseAddress ??= BaseAddress.DefaultUri;
    }

    /// <summary>Retrieves information about the current access token.</summary>
    /// <param name="accessToken">An API key or subtoken.</param>
    /// <param name="missingMemberBehavior">The desired behavior when JSON contains unexpected members.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    /// <returns>A task that represents the API request.</returns>
    public Task<(TokenInfo Value, MessageContext Context)> GetTokenInfo(
        string accessToken,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        TokenInfoRequest request = new(accessToken)
        {
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
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
    public Task<(CreatedSubtoken Value, MessageContext Context)> CreateSubtoken(
        string accessToken,
        IEnumerable<Permission>? permissions = null,
        DateTimeOffset? absoluteExpirationDate = null,
        IEnumerable<string>? allowedUrls = null,
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        CreateSubtokenRequest request = new(accessToken)
        {
            Permissions = permissions?.ToList(),
            AbsoluteExpirationDate = absoluteExpirationDate,
            AllowedUrls = allowedUrls?.ToList(),
            MissingMemberBehavior = missingMemberBehavior
        };
        return request.SendAsync(httpClient, cancellationToken);
    }
}
