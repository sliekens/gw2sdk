namespace GuildWars2.Skills;

[PublicAPI]
public sealed record WeaponSkill : Skill
{
    public required Attunement? Attunement { get; init; }

    public required Attunement? DualAttunement { get; init; }

    public required int? Cost { get; init; }

    public required Offhand? Offhand { get; init; }

    public required int? Initiative { get; init; }
}
