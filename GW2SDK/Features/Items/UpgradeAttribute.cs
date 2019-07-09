using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class UpgradeAttribute
    {
        public AttributeName Attribute { get; set; }

        public int Modifier { get; set; }
    }
}