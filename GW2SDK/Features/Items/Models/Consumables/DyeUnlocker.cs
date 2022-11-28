using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record DyeUnlocker : Unlocker
{
    public required int ColorId { get; init; }
}
