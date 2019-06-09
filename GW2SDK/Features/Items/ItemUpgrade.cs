using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [DataTransferObject(RootObject = false)]
    public sealed class ItemUpgrade
    {
        public UpgradeType Upgrade { get; set; }

        public int ItemId { get; set; }
    }
}
