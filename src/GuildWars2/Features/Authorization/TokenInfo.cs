namespace GuildWars2.Authorization;

/// <summary>Information about the access token. This is a base type, derived types exist for API keys and subtokens. Cast
/// objects of this type to a more specific type to access more properties.</summary>
[Inheritable]
[DataTransferObject]
public record TokenInfo
{
    /// <summary>The names of all permissions.</summary>
    public static readonly IImmutableValueList<Extensible<Permission>> AllPermissions = new ImmutableValueList<Extensible<Permission>>(
        [
            Permission.Account,
            Permission.Builds,
            Permission.Characters,
            Permission.Guilds,
            Permission.Inventories,
            Permission.Progression,
            Permission.PvP,
            Permission.Unlocks,
            Permission.Wallet,
            Permission.TradingPost,
            Permission.Wvw
        ]
    );

    /// <summary>The ID of the (root) API key.</summary>
    public required string Id { get; init; }

    /// <summary>The name of the (root) API key.</summary>
    public required string Name { get; init; }

    /// <summary>The allowed permissions.</summary>
    public required IImmutableValueList<Extensible<Permission>> Permissions { get; init; }
}
