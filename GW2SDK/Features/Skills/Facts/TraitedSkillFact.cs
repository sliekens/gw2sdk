namespace GuildWars2.Skills.Facts;

[PublicAPI]
[DataTransferObject]
public sealed record TraitedSkillFact
{
    /// <summary>The ID of the trait that activates this combination.</summary>
    public required int RequiresTrait { get; init; }

    /// <summary>The index of the fact that is replaced by this <see cref="Fact" />, or <c>null</c> if it is to be appended to
    /// the existing facts.</summary>
    public required int? Overrides { get; init; }

    public required SkillFact Fact { get; init; }
}
