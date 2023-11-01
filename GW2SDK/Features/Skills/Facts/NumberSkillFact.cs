namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record NumberSkillFact : SkillFact
{
    public required int Value { get; init; }
}
