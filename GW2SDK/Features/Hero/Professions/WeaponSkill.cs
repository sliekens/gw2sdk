namespace GuildWars2.Hero.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record WeaponSkill
{
    public required int Id { get; init; }

    public required SkillSlot Slot { get; init; }

    public required Offhand? Offhand { get; init; }

    public required Attunement? Attunement { get; init; }
}
