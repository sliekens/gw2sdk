namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record RangeSkillFact : SkillFact
{
    public required int Value { get; init; }
}
