using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

/// <summary>A skill reference with limited details such as the skill slot. The <see cref="Id" /> can be used to fetch the
/// full skill object.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record SkillReference
{
    public required int Id { get; init; }

    public required SkillSlot Slot { get; init; }
}
