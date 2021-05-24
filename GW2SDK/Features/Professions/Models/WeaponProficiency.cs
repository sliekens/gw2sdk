using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Professions
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record WeaponProficiency
    {
        public int? RequiredSpecialization { get; init; }

        public WeaponFlag[] Flags { get; init; } = Array.Empty<WeaponFlag>();

        public WeaponSkill[] Skills { get; init; } = Array.Empty<WeaponSkill>();
    }
}