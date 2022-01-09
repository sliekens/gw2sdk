using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record PetSkillBar
    {
        public int?[] Terrestrial { get; init; } = Array.Empty<int?>();

        public int?[] Aquatic { get; init; } = Array.Empty<int?>();
    }
}