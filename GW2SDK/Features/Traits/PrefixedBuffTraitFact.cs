using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class PrefixedBuffTraitFact : BuffTraitFact
    {
        public BuffPrefix Prefix { get; set; } = new BuffPrefix();
    }
}
