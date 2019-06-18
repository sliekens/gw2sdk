using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
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