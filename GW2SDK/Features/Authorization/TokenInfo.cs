namespace GuildWars2.Authorization;

/// <summary>Information about the access token. This is a base type, derived types exist for API keys and subtokens. Cast
/// objects of this type to the derived type for additional information</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record TokenInfo
{
    /// <summary>The ID of the (root) API key.</summary>
    public required string Id { get; init; }

    /// <summary>The name of the (root) API key.</summary>
    public required string Name { get; init; }

    /// <summary>The allowed permissions.</summary>
    public required IReadOnlyCollection<Permission> Permissions { get; init; }
}
