namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record RechargeSkillFact : SkillFact
{
    public required double Value { get; init; }
}
