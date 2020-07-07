using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ComboFieldTraitFact : TraitFact
    {
        [JsonProperty("field_type", Required = Required.Always)]
        public ComboFieldName Field { get; set; }
    }
}