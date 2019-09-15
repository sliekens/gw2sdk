using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class CreatedSubtoken
    {
        [JsonProperty(Required = Required.Always)]
        public string Subtoken { get; set; }
    }
}
