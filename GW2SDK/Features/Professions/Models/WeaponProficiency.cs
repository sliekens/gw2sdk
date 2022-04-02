using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Professions
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record WeaponProficiency
    {
        public int? RequiredSpecialization { get; init; }

        public IReadOnlyCollection<WeaponFlag> Flags { get; init; } = Array.Empty<WeaponFlag>();

        public IReadOnlyCollection<WeaponSkill> Skills { get; init; } = Array.Empty<WeaponSkill>();
    }
}
