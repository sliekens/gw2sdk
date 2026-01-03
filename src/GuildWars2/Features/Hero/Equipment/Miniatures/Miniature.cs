using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Miniatures;

/// <summary>Information about a miniature.</summary>
[DataTransferObject]
[JsonConverter(typeof(MiniatureJsonConverter))]
public sealed record Miniature
{
    /// <summary>The miniature ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the miniature.</summary>
    public required string Name { get; init; }

    /// <summary>A description of how to obtain the miniature, as it appears in the tooltip of a locked miniature.</summary>
    public required string LockedText { get; init; }

    /// <summary>The URL of the miniature icon.</summary>
    [Obsolete("Use IconUrl instead.")]
    public required string IconHref { get; init; }

    /// <summary>The URL of the miniature icon.</summary>
    public required Uri IconUrl { get; init; }

    /// <summary>The display order of the miniature in the equipment panel.</summary>
    public required int Order { get; init; }

    /// <summary>The ID of the item that summons the miniature.</summary>
    public required int ItemId { get; init; }
}
