using JetBrains.Annotations;

namespace GW2SDK.Items;

[PublicAPI]
public sealed record Minipet : Item
{
    public required int MinipetId { get; init; }
}
