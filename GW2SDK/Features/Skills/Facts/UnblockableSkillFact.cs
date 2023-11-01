namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record UnblockableSkillFact : SkillFact
{
    public required bool Value { get; init; }
}
