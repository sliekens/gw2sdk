namespace GuildWars2.Items.Stats;

/// <summary>Information about an item attribute combination like Soldier.</summary>
[DataTransferObject]
public sealed record AttributeCombination
{
    /// <summary>The ID of the attribute combination.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the attribute combination.</summary>
    public required string Name { get; init; }

    /// <summary>The attributes values which are used to calculate the final stats of an item.</summary>
    public required IReadOnlyCollection<Attribute> Attributes { get; init; }
}
