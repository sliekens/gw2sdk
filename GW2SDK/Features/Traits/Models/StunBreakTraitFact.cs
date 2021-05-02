﻿using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record StunBreakTraitFact : TraitFact
    {
        public bool Value { get; init; }
    }
}