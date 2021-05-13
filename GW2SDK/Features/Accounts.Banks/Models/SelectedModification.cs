﻿using GW2SDK.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record SelectedModification
    {
        public int? AgonyResistance { get; init; }

        public int? BoonDuration { get; init; }

        public int? ConditionDamage { get; init; }

        public int? ConditionDuration { get; init; }

        public int? CritDamage { get; init; }

        public int? Healing { get; init; }

        public int? Power { get; init; }

        public int? Precision { get; init; }

        public int? Toughness { get; init; }

        public int? Vitality { get; init; }
    }
}
