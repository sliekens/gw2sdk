using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record PvpEquipment
    {
        public int? AmuletId { get; init; }

        public int? RuneId { get; init; }

        public int?[] SigilIds { get; init; } = Array.Empty<int?>();
    }
}