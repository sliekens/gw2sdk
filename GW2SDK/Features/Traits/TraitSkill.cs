namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record TraitSkill
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IReadOnlyCollection<TraitFact> Facts { get; init; }

    public required IReadOnlyCollection<CompoundTraitFact>? TraitedFacts { get; init; }

    public required string Description { get; init; }

    public required string Icon { get; init; }

    public required string ChatLink { get; init; }

    public required IReadOnlyCollection<SkillCategoryName>? Categories { get; init; }
}
