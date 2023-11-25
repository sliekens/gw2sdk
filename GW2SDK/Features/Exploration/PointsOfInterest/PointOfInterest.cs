using System.Drawing;

namespace GuildWars2.Exploration.PointsOfInterest;

/// <summary>Information about a notable location on the map, such as a vista, a point of interest, a hero challenge, or an
/// adventure. This class is the base type for all of those. Cast to a more specific type to access the properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record PointOfInterest
{
    /// <summary>The name as displayed on the map, or an empty string if the point doesn't have a name.</summary>
    public required string Name { get; init; }

    /// <summary>The point of interest ID.</summary>
    public required int Id { get; init; }

    /// <summary>The floor on which the point of interest is located.</summary>
    public required int Floor { get; init; }

    /// <summary>The map coordinates of the point of interest.</summary>
    public required PointF Coordinates { get; init; }

    /// <summary>The chat code of the point of interest. This can be used to link the location in the chat, but also in guild
    /// or squad messages.</summary>
    public required string ChatLink { get; init; }
}
