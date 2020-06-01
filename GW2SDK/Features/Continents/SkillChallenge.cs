using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class SkillChallenge
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; } = "";

        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; } = new double[0];
    }
}
