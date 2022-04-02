using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record PetSkillBar
    {
        public IReadOnlyCollection<int?> Terrestrial { get; init; } = Array.Empty<int?>();

        public IReadOnlyCollection<int?> Aquatic { get; init; } = Array.Empty<int?>();
    }
}
