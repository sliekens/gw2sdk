using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Currencies;

[PublicAPI]
[DataTransferObject]
public sealed record Currency
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public int Order { get; init; }

    public string Icon { get; init; } = "";
}
