﻿using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record AttributeAdjustTraitFact : TraitFact
    {
        public int Value { get; init; }

        public TraitTarget Target { get; init; }
    }
}