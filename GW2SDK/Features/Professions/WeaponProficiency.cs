using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
[DataTransferObject]
public sealed record WeaponProficiency
{
    public required int? RequiredSpecialization { get; init; }

    public required IReadOnlyCollection<WeaponFlag> Flags { get; init; }

    public required IReadOnlyCollection<WeaponSkill> Skills { get; init; }
}
