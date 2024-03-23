using System.Drawing;
using System.Numerics;
using GuildWars2.Chat;
using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Objectives;

[PublicAPI]
[DataTransferObject]
public sealed record Objective
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required int SectorId { get; init; }

    public required ObjectiveKind Kind { get; init; }

    public required MapKind MapKind { get; init; }

    public required int MapId { get; init; }

    public required int? UpgradeId { get; init; }

    public required Vector3? Coordinates { get; init; }

    public required PointF? LabelCoordinates { get; init; }

    public required string MarkerHref { get; init; }

    /// <summary>The chat code of the objective. This can be used to link the objective in the chat, but also in guild or squad
    /// messages.</summary>
    public required string ChatLink { get; init; }

    /// <summary>Gets a chat link object for this objective.</summary>
    /// <returns>The chat link as an object.</returns>
    public ObjectiveLink GetChatLink() =>
        new()
        {
            ObjectiveId = int.Parse(Id.Split('-')[1]),
            MapId = MapId
        };
}
