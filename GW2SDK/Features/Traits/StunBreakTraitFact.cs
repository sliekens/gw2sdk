﻿using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class StunBreakTraitFact : TraitFact
    {
        [JsonProperty("value", Required = Required.Always)]
        public bool Value { get; set; }
    }
}