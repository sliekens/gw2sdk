using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class InfixUpgrade
    {
        public int Id { get; set; }

        [NotNull]
        public UpgradeAttribute[] Attributes { get; set; }

        [CanBeNull]
        public Buff Buff { get; set; }
    }
}