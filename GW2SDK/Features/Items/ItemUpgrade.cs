using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class ItemUpgrade
    {
        public UpgradeType Upgrade { get; set; }

        public int ItemId { get; set; }
    }
}
