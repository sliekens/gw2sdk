namespace GuildWars2.Hero.Accounts;

/// <summary>Information about a character's WvW ability training progress.</summary>
[DataTransferObject]
public sealed record WvwAbility
{
    /// <summary>The ID of the current ability.</summary>
    public required int Id { get; init; }

    /// <summary>The current rank of the ability.</summary>
    public required int Rank { get; init; }
}
