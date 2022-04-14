using JetBrains.Annotations;

namespace GW2SDK.Maps.Models;

[PublicAPI]
public sealed record UnlockerPointOfInterest : PointOfInterest
{
    public string Icon { get; init; } = "";
}