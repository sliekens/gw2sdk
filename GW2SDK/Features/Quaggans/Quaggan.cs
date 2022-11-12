using JetBrains.Annotations;

namespace GW2SDK.Quaggans;

[PublicAPI]
public sealed record Quaggan
{
    public required string Id { get; init; }

    public required string PictureHref { get; init; }
}
