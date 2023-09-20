namespace GuildWars2.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record WeaponProficiency
{
    public required int? RequiredSpecialization { get; init; }

    public required IReadOnlyCollection<WeaponFlag> Flags { get; init; }

    public required IReadOnlyCollection<WeaponSkill> Skills { get; init; }
}
