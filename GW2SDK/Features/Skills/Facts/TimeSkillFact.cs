namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record TimeSkillFact : SkillFact
{
    public required TimeSpan Duration { get; init; }
}
