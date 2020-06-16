using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Backstories.Answers
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class BackstoryAnswer
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Journal { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public int Question { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public Race[]? Races { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public Profession[]? Professions { get; set; }
    }
}
