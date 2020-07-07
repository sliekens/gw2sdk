﻿using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class RechargeTraitFact : TraitFact
    {
        [JsonProperty("value", Required = Required.Always)]
        public int Value { get; set; }
    }
}