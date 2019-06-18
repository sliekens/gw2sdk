using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class UpgradeAttribute
    {
        public AttributeName Attribute { get; set; }

        public int Modifier { get; set; }
    }
}