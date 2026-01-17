namespace GuildWars2.Pvp.MistChampions;

/// <summary>Information about a Mist Champion.</summary>
[DataTransferObject]
public sealed record MistChampion
{
    /// <summary>The Mist Champion's ID.</summary>
    public required string Id { get; init; }

    /// <summary>The Mist Champion's name.</summary>
    public required string Name { get; init; }

    /// <summary>The Mist Champion's background story.</summary>
    public required string Description { get; init; }

    /// <summary>The Mist Champion's type description.</summary>
    public required string Type { get; init; }

    /// <summary>The Mist Champion's stats.</summary>
    public required MistChampionStats Stats { get; init; }

    /// <summary>The URL of the Mist Champion's overlay image.</summary>
    public required Uri OverlayImageUrl { get; init; }

    /// <summary>The URL of the Mist Champion's underlay image.</summary>
    public required Uri UnderlayImageUrl { get; init; }

    /// <summary>The Mist Champion's skins.</summary>
    public required IImmutableValueList<MistChampionSkin> Skins { get; init; }
}
