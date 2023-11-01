namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record DurationSkillFact : SkillFact
{
    public required TimeSpan Duration { get; init; }
}
