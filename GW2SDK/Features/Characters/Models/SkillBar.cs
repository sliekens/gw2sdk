using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record SkillBar
    {
        public int? Heal { get; init; }

        // Always length 3
        public IReadOnlyCollection<int?> Utilities { get; init; } = Array.Empty<int?>();

        public int? Elite { get; init; }
    }
}