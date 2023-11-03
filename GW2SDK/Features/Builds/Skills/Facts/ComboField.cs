namespace GuildWars2.Builds.Skills.Facts;

/// <summary>A combo field that is placed by the skill.</summary>
[PublicAPI]
public sealed record ComboField : SkillFact
{
    /// <summary>The kind of field that is created.</summary>
    public required ComboFieldName Field { get; init; }
}
