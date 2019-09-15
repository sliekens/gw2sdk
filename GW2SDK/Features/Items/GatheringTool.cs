﻿using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items.Impl;
using Newtonsoft.Json;

namespace GW2SDK.Items
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(GatheringToolDiscriminatorOptions))]
    public class GatheringTool : Item
    {
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }
    }
}
