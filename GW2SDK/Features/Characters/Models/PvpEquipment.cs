using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record PvpEquipment
    {
        /// <summary>The ID of the selected amulet.</summary>
        public int? AmuletId { get; init; }

        /// <summary>The ID of the selected rune.</summary>
        public int? RuneId { get; init; }

        /// <summary>The IDs of all equipped sigils.</summary>
        public int?[] SigilIds { get; init; } = Array.Empty<int?>();
    }
}
