using GuildWars2.Hero.Builds;

namespace GuildWars2.Hero.Equipment.Mounts;

/// <summary>A reference to a skill.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SkillReference
{
    /// <summary>The skill ID.</summary>
    public required int Id { get; init; }

    /// <summary>The slot occupied by the skill.</summary>
    public required Extensible<SkillSlot> Slot { get; init; }
}
