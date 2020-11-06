using System.Collections.Generic;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Characters.Recipes
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class Character
    {
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<int> Recipes { get; set; } = new List<int>(0);
    }
}
