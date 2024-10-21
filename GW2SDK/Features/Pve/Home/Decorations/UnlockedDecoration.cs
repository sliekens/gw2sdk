namespace GuildWars2.Pve.Home.Decorations;

/// <summary>Information about a decoration unlocked on the account.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record UnlockedDecoration
{
    /// <summary>The decoration ID.</summary>
    public required int Id { get; init; }

    /// <summary>The number of this decoration that is owned.</summary>
    public required int Count { get; init; }
}
