namespace GuildWars2.Hero.Equipment.JadeBots;

/// <summary>Information about a jade bot.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record JadeBot
{
    /// <summary>The jade bot ID.</summary>
    public required int Id { get; init; }

    /// <summary>The jade bot name.</summary>
    public required string Name { get; init; }

    /// <summary>The jade bot description.</summary>
    public required string Description { get; init; }

    /// <summary>The ID of the item which grants this jade bot when consumed.</summary>
    public required int UnlockItemId { get; init; }
}
