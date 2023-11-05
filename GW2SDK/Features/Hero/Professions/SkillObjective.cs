namespace GuildWars2.Hero.Professions;

[PublicAPI]
public sealed record SkillObjective : TrainingObjective
{
    public required int SkillId { get; init; }
}
