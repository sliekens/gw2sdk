using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Crafting.Models;

[PublicAPI]
[DataTransferObject]
public sealed record GuildIngredient
{
    public int UpgradeId { get; init; }

    public int Count { get; init; }
}