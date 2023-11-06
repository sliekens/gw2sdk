namespace GuildWars2.Hero.Training;

[PublicAPI]
public sealed record SkillObjective : TrainingObjective
{
    public required int SkillId { get; init; }
}
