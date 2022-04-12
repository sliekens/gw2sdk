using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record WeaponSkill
{
    public int Id { get; init; }

    public SkillSlot Slot { get; init; }

    public Offhand? Offhand { get; init; }

    public Attunement? Attunement { get; init; }
}