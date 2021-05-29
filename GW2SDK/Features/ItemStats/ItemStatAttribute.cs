using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.ItemStats
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ItemStatAttribute
    {
        public UpgradeAttributeName Attribute { get; init; }

        public double Multiplier { get; init; }

        public int Value { get; init; }
    }
}
