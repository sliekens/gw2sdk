namespace GuildWars2.Hero.Builds.Facts;

/// <summary>A combo field that is placed by the skill/trait.</summary>
[PublicAPI]
public sealed record ComboField : Fact
{
    /// <summary>The kind of field that is created.</summary>
    public required ComboFieldName Field { get; init; }
}
