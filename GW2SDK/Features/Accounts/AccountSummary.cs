namespace GuildWars2.Accounts;

/// <summary>Information about a player account.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record AccountSummary
{
    /// <summary>The unique and immutable ID of the account.</summary>
    public required string Id { get; init; }

    /// <summary>The unique name of the account. It is created when first setting up an account by combining a chosen username
    /// with a randomly generated four-digit number. The resulting Display Name serves as a master identifier for the account:
    /// allowing users to add you to their social lists (friends, guild, block, etc.) and send you in-game mail, and serving as
    /// your forum identity when accessing and posting on the official forum.</summary>
    /// <remarks>The Display Name can be changed once every 90 days. Use <see cref="Id" /> if you need an immutable identifier.</remarks>
    public required string DisplayName { get; init; }

    /// <summary>The duration played across all characters on this account. Precision: 1 minute.</summary>
    public required TimeSpan Age { get; init; }

    /// <summary>The date and time when this summary was generated.</summary>
    public required DateTimeOffset LastModified { get; init; }

    /// <summary>The ID of the account's home world.</summary>
    public required int WorldId { get; init; }

    /// <summary>The IDs of the guilds that the account is a member of (in any role).</summary>
    public required IReadOnlyCollection<string> GuildIds { get; init; }

    /// <summary>The IDs of the guilds that the account is the leader of. Requires the 'guilds' scope.</summary>
    public required IReadOnlyCollection<string>? LeaderOfGuildIds { get; init; }

    /// <summary>The date and time when the account was created.</summary>
    public required DateTimeOffset Created { get; init; }

    /// <summary>Indicates what content can be accessed with this account.</summary>
    public required IReadOnlyCollection<ProductName> Access { get; init; }

    /// <summary>Indicates if the account has unlocked the commander tag.</summary>
    public required bool Commander { get; init; }

    /// <summary>The account's personal fractal reward level. Requires the 'progression' scope.</summary>
    public required int? FractalLevel { get; init; }

    /// <summary>The number of points gained in the Daily achievement category. Requires the 'progression' scope.</summary>
    public required int? DailyAchievementPoints { get; init; }

    /// <summary>The number of points gained in the Monthly achievement category (historical). Requires the 'progression'
    /// scope.</summary>
    public required int? MonthlyAchievementPoints { get; init; }

    /// <summary>The account's personal World vs. World rank. Requires the 'progression' scope.</summary>
    public required int? WvwRank { get; init; }

    /// <summary>The count of unlocked build storage slots. Requires the 'builds' scope.</summary>
    public required int? BuildStorageSlots { get; init; }
}
