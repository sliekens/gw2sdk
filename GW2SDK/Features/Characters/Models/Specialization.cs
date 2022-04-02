using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Specialization
    {
        public int? Id { get; init; }

        // Always length 3
        public IReadOnlyCollection<int?> Traits { get; init; } = Array.Empty<int?>();
    }
}
