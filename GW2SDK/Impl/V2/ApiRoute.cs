using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Impl.V2
{
    [DebuggerDisplay("{Path,nq}")]
    [DataTransferObject(RootObject = false)]
    internal sealed class ApiRoute
    {
        [JsonProperty("path", Required = Required.Always)]
        public string Path { get; set; } = "";

        [JsonProperty("lang", Required = Required.Always)]
        public bool Multilingual { get; set; }

        [JsonProperty("auth", Required = Required.Always)]
        public bool RequiresAuthorization { get; set; }

        [JsonProperty("active", Required = Required.Always)]
        public bool Active { get; set; }
    }
}
