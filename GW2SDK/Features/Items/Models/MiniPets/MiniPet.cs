using JetBrains.Annotations;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record Minipet : Item
{
    public required int MinipetId { get; init; }
}
