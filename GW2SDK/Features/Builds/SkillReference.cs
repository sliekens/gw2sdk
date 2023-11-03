namespace GuildWars2.Builds;

/// <summary>References a skill by ID and includes attunement or transformation requirement.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SkillReference
{
    /// <summary>The skill ID.</summary>
    public required int Id { get; init; }

    /// <summary>Used for Elementalist skills to indicate which attunement this skill is associated with.</summary>
    public required Attunement? Attunement { get; init; }

    /// <summary>Used to indicate which transformation this skill is associated with.</summary>
    public required Transformation? Form { get; init; }
}
