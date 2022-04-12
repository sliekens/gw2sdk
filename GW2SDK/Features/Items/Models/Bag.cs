using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record Bag : Item
{
    public bool NoSellOrSort { get; init; }

    public int Size { get; init; }
}