using GW2SDK.Annotations;
using GW2SDK.Enums;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class UpgradeAttribute
    {
        public UpgradeAttributeName Attribute { get; set; }

        public int Modifier { get; set; }
    }
}
