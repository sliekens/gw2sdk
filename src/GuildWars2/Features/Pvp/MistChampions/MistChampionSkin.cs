namespace GuildWars2.Pvp.MistChampions;

/// <summary>Information about a Mist Champion skin.</summary>
[DataTransferObject]
public sealed record MistChampionSkin
{
    /// <summary>The skin ID.</summary>
    public required int Id { get; init; }

    /// <summary>The skin name.</summary>
    public required string Name { get; init; }

    /// <summary>The URL of the skin icon as a Uri.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>Indicates whether this is the default skin for the associated Mist Champion.</summary>
    public required bool Default { get; init; }

    /// <summary>The IDs of the items that unlock the skin when consumed.</summary>
    public required IImmutableValueList<int> UnlockItemIds { get; init; }
}
