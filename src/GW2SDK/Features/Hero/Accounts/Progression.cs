namespace GuildWars2.Hero.Accounts;

/// <summary>The Id and the amount of a progression kind on the account.</summary>
public sealed record Progression
{
    /// <summary>The progression kind. See <see cref="ProgressionKind" /> for known values.</summary>
    public required string Id { get; init; }

    /// <summary>The amount of progression.</summary>
    public required int Value { get; init; }
}
