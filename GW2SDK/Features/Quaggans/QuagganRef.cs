using JetBrains.Annotations;

namespace GW2SDK.Quaggans
{
    [PublicAPI]
    public sealed record QuagganRef
    {
        public string Id { get; init; } = "";

        public string PictureHref { get; init; } = "";
    }
}
