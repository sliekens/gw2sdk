using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record Bag : Item
{
    public required bool NoSellOrSort { get; init; }

    public required int Size { get; init; }
}
