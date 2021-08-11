using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Specialization
    {
        public int? Id { get; init; }

        // Always length 3
        public int?[] Traits { get; init; } = Array.Empty<int?>();
    }
}