namespace GuildWars2.Skills;

[PublicAPI]
public sealed record HealSkill : Skill
{
    public required int? ToolbeltSkill { get; init; }

    public required Attunement? Attunement { get; init; }

    public required int? Cost { get; init; }

    public required IReadOnlyCollection<int>? BundleSkills { get; init; }

    public required IReadOnlyCollection<SkillReference>? Subskills { get; init; }
}
