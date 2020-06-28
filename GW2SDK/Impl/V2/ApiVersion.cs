using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Impl.V2
{
    [DebuggerDisplay("{Version,nq}")]
    [DataTransferObject(RootObject = false)]
    internal sealed class ApiVersion
    {
        [JsonProperty("v", Required = Required.Always)]
        public string Version { get; set; } = "latest";

        [JsonProperty("desc", Required = Required.Always)]
        public string Description { get; set; } = "";
    }
}
