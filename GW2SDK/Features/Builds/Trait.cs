namespace GuildWars2.Builds;

[PublicAPI]
[DataTransferObject]
public sealed record Trait
{
    public required int Id { get; init; }

    public required int Tier { get; init; }

    public required int Order { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required TraitSlot Slot { get; init; }

    public required IReadOnlyCollection<Fact>? Facts { get; init; }

    public required IReadOnlyCollection<TraitedFact>? TraitedFacts { get; init; }

    public required IReadOnlyCollection<Skill>? Skills { get; init; }

    public required int SpezializationId { get; init; }

    public required string Icon { get; init; }
}
