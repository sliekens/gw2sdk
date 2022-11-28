using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Skins;

[PublicAPI]
[DataTransferObject]
public sealed record DyeSlot
{
    public required int ColorId { get; init; }

    public required Material Material { get; init; }
}
