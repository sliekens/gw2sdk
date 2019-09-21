﻿using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    public class Container : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }
    }
}
