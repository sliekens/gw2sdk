using JetBrains.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    public sealed record Rune : UpgradeComponent
    {
        public string[]? Bonuses { get; init; }
    }
}
