using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Backstories.Questions
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class BackstoryQuestion
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Title { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Description { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string[] Answers { get; set; } = new string[0];

        [JsonProperty(Required = Required.Always)]
        public int Order { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public Race[]? Races { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public Profession[]? Professions { get; set; }
    }
}
