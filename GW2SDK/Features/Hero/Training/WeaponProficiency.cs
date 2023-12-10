﻿namespace GuildWars2.Hero.Training;

[PublicAPI]
[DataTransferObject]
public sealed record WeaponProficiency
{
    public required int? RequiredSpecialization { get; init; }

    public required WeaponFlags Flags { get; init; }

    public required IReadOnlyCollection<WeaponSkill> Skills { get; init; }
}
