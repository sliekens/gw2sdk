using JetBrains.Annotations;

namespace GW2SDK.Quaggans
{
    [PublicAPI]
    public sealed record Quaggan
    {
        public string Id { get; init; } = "";

        public string PictureHref { get; init; } = "";
    }
}
