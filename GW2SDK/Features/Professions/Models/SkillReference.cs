using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Professions;

/// <summary>A skill reference with limited details such as the skill slot. The <see cref="Id" /> can be used to fetch the
/// full skill object.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record SkillReference
{
    public int Id { get; init; }

    public SkillSlot Slot { get; init; }
}