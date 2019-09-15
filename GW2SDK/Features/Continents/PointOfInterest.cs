using GW2SDK.Annotations;
using GW2SDK.Continents.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [Inheritable]
    [JsonConverter(typeof(DiscriminatedJsonConverter), typeof(PointOfInterestDiscriminatorOptions))]
    [DataTransferObject(RootObject = false)]
    public class PointOfInterest
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int Floor { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public int[] Coordinates { get; set; }
        
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; }
    }
}