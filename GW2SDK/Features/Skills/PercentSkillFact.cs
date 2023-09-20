namespace GuildWars2.Skills;

[PublicAPI]
public sealed record PercentSkillFact : SkillFact
{
    public required double Percent { get; init; }
}
