using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record EquipmentTab
    {
        /// <summary>The number of the current tab.</summary>
        public int Tab { get; init; }

        /// <summary>The player-chosen name of this equipment tab.</summary>
        public string Name { get; init; } = "";

        /// <summary>The selected equipment on this tab.</summary>
        public IEnumerable<EquipmentItem> Equipment { get; init; } = Array.Empty<EquipmentItem>();

        /// <summary>The selected PvP equipment on this tab.</summary>
        public PvpEquipment PvpEquipment { get; init; } = new();
    }
}
