using JetBrains.Annotations;

namespace GuildWars2.Quaggans;

[PublicAPI]
public sealed record Quaggan
{
    public required string Id { get; init; }

    public required string PictureHref { get; init; }
}
