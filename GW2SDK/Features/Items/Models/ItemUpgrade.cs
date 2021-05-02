using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ItemUpgrade
    {
        public UpgradeType Upgrade { get; init; }

        public int ItemId { get; init; }
    }
}
