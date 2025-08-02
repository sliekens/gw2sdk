using System.Drawing;
using System.Globalization;
using System.Numerics;

using GuildWars2.Chat;
using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Objectives;

/// <summary>Information about an objective in World vs. World. This class is the base type. Cast objects of this type to a
/// more specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Objective
{
    /// <summary>The objective ID.</summary>
    public required string Id { get; init; }

    /// <summary>The objective name.</summary>
    public required string Name { get; init; }

    /// <summary>The ID of the map sector that contains the objective.</summary>
    public required int SectorId { get; init; }

    /// <summary>The map kind of the map that contains the objective.</summary>
    public required Extensible<MapKind> MapKind { get; init; }

    /// <summary>The ID of the map that contains the objective. This can be used to look up the map in the maps endpoint.</summary>
    public required int MapId { get; init; }

    /// <summary>The ID of the upgrade that the objective has. This can be used to look up the upgrade in the WvW upgrades
    /// endpoint.</summary>
    public required int? UpgradeId { get; init; }

    /// <summary>The coorinates of the objective on the map.</summary>
    public required Vector3? Coordinates { get; init; }

    /// <summary>The coordinates of the label on the map.</summary>
    public required PointF? LabelCoordinates { get; init; }

    /// <summary>The URL of the objective's icon.</summary>
    [Obsolete("Use MarkerIconUrl instead.")]
    public required string MarkerIconHref { get; init; }

    /// <summary>The URL of the objective's icon.</summary>
    public required Uri? MarkerIconUrl { get; init; }

    /// <summary>The chat code of the objective. This can be used to link the objective in the chat, but also in guild or squad
    /// messages.</summary>
    public required string ChatLink { get; init; }

    /// <summary>Gets a chat link object for this objective.</summary>
    /// <returns>The chat link as an object.</returns>
    public ObjectiveLink GetChatLink()
    {
        return new()
        {
            ObjectiveId = int.Parse(Id.Split('-')[1], CultureInfo.InvariantCulture),
            MapId = MapId
        };
    }
}
