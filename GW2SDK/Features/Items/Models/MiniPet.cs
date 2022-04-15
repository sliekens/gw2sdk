using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record Minipet : Item
{
    public int MinipetId { get; init; }
}
