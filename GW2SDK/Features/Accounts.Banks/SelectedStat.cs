using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class SelectedStat
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public SelectedModification Attributes { get; set; } = new SelectedModification();
    }
}
