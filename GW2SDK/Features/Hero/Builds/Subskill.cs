namespace GuildWars2.Hero.Builds;

/// <summary>Information about skills which have alternate function under certain circumstances, but share the same
/// cooldown with the main skill. For example Elementalist glyps have different effects based on the active attunement.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Subskill
{
    /// <summary>The subskill ID.</summary>
    public required int Id { get; init; }

    /// <summary>Used for Elementalist skills with alternate function based on the active attunement.</summary>
    public required Attunement? Attunement { get; init; }

    /// <summary>Used for Druid skills with alternate function based on the active transformation.</summary>
    public required Transformation? Form { get; init; }
}
