using System.Drawing;
using GuildWars2.Chat;

namespace GuildWars2.Exploration.GodShrines;

/// <summary>Information about a god shrine which are found in the Ruins of Orr, for example the Temple of Balthazar in the
/// Straits of Devastation.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GodShrine
{
    /// <summary>The god shrine ID.</summary>
    public required int Id { get; init; }

    /// <summary>The PoI ID associated with the god shrine.</summary>
    public required int PointOfInterestId { get; init; }

    /// <summary>The name of the god shrine when it is uncontested.</summary>
    public required string Name { get; init; }

    /// <summary>The name of the god shrine when it is contested.</summary>
    public required string NameContested { get; init; }

    /// <summary>The URL of the god shrine icon when it is uncontested.</summary>
    public required string IconHref { get; init; }

    /// <summary>The URL of the god shrine icon when it is contested.</summary>
    public required string IconContestedHref { get; init; }

    /// <summary>The map coordinates of the god shrine.</summary>
    public required PointF Coordinates { get; init; }

    /// <summary>Gets a chat link object for this god shrine.</summary>
    /// <returns>The chat link as an object.</returns>
    public PointOfInterestLink GetChatLink() => new() { PointOfInterestId = Id };
}
