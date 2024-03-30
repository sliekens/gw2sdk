using GuildWars2.Hero.Builds;

namespace GuildWars2.Hero.Training;

/// <summary>Short summary of a skill. This type is the base type. Cast objects of this type to a more specific type to
/// access more properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record SkillSummary
{
    /// <summary>The skill ID.</summary>
    public required int Id { get; init; }

    /// <summary>The slot in which the skill can be equipped.</summary>
    public required Extensible<SkillSlot> Slot { get; init; }
}
