using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record Bag : Item
{
    public required bool NoSellOrSort { get; init; }

    public required int Size { get; init; }
}
