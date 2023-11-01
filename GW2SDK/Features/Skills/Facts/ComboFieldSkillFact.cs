namespace GuildWars2.Skills.Facts;

[PublicAPI]
public sealed record ComboFieldSkillFact : SkillFact
{
    public required ComboFieldName Field { get; init; }
}
