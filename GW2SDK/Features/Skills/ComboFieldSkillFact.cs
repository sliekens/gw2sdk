namespace GuildWars2.Skills;

[PublicAPI]
public sealed record ComboFieldSkillFact : SkillFact
{
    public required ComboFieldName Field { get; init; }
}
