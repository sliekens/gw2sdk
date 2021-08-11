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
        public int Tab { get; init; }

        public string Name { get; init; } = "";

        public IEnumerable<EquipmentItem> Equipment { get; init; } = Array.Empty<EquipmentItem>();

        public PvpEquipment PvpEquipment { get; init; } = new ();
    }
}