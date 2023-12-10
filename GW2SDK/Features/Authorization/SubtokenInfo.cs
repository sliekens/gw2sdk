namespace GuildWars2.Authorization;

/// <summary>Information about a subtoken.</summary>
[PublicAPI]
public sealed record SubtokenInfo : TokenInfo
{
    /// <summary>The absolute expiration date of the subtoken.</summary>
    public required DateTimeOffset ExpiresAt { get; init; }

    /// <summary>The creation date of the subtoken.</summary>
    public required DateTimeOffset IssuedAt { get; init; }

    /// <summary>The list of allowed URLs which may be accessed with the token. If this is not present, all URLs are allowed.</summary>
    public required IReadOnlyCollection<Uri>? Urls { get; init; }
}
