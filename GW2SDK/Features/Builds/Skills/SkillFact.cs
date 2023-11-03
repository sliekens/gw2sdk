namespace GuildWars2.Builds.Skills;

/// <summary>An effect applied by the skill.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record SkillFact
{
    /// <summary>A brief summary of the skill effect</summary>
    public required string Text { get; init; }

    /// <summary>The icon as it appears in the tooltip of the skill.</summary>
    public required string Icon { get; init; }
}
