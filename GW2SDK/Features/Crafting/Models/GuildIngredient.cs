using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
[DataTransferObject]
public sealed record GuildIngredient
{
    public required int UpgradeId { get; init; }

    public required int Count { get; init; }
}
