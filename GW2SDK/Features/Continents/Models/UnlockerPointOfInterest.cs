using JetBrains.Annotations;

namespace GW2SDK.Continents
{
    [PublicAPI]
    public sealed record UnlockerPointOfInterest : PointOfInterest
    {
        public string Icon { get; init; } = "";
    }
}
