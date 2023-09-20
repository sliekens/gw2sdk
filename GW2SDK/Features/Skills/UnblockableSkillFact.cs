namespace GuildWars2.Skills;

[PublicAPI]
public sealed record UnblockableSkillFact : SkillFact
{
    public required bool Value { get; init; }
}
