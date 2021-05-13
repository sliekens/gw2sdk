using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record DefaultUpgradeComponent : UpgradeComponent
    {
        public string[]? Bonuses { get; init; }
    }
}
