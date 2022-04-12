using JetBrains.Annotations;

namespace GW2SDK.Maps;

[PublicAPI]
public sealed record UnlockerPointOfInterest : PointOfInterest
{
    public string Icon { get; init; } = "";
}