using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Minipets;

[PublicAPI]
[DataTransferObject]
public sealed record Minipet
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Unlock { get; init; } = "";

    public string Icon { get; init; } = "";

    public int Order { get; init; }

    public int ItemId { get; init; }
}
