using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record PrefixedBuffTraitFact : BuffTraitFact
    {
        public BuffPrefix Prefix { get; init; } = new();
    }
}
