using JetBrains.Annotations;

namespace GW2SDK.Skills;

[PublicAPI]
public sealed record WeaponSkill : Skill
{
    public Attunement? Attunement { get; init; }

    public Attunement? DualAttunement { get; init; }

    public int? Cost { get; init; }

    public Offhand? Offhand { get; init; }

    public int? Initiative { get; init; }
}
