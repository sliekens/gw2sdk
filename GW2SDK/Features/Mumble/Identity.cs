using GuildWars2.Hero;

namespace GuildWars2.Mumble;

/// <summary>Information about the player character's identity provided by the MumbleLink API.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Identity
{
    /// <summary>The current character's name.</summary>
    public required string Name { get; init; }

    /// <summary>The current character's profession.</summary>
    public required ProfessionName Profession { get; init; }

    /// <summary>The ID of the current character's Elite specialization slot.</summary>
    /// <remarks>The third specialization slot is the Elite slot, which is available starting at level 71. Elite
    /// specializations can only be used in this slot, but the slot itself can contain any specialization.</remarks>
    public required int SpecializationId { get; init; }

    /// <summary>The current character's race.</summary>
    public required RaceName Race { get; init; }

    /// The ID of the current character's map.
    public required int MapId { get; init; }

    /// The ID of the current character's world.
    public required long WorldId { get; init; }

    /// The current character's team color. It is the ID of a dye color that can be resolved from the API. 0 means neutral / white.
    public required int TeamColorId { get; init; }

    /// <summary>Indicates whether the player is currently the leader of a squad.</summary>
    public required bool Commander { get; init; }

    /// <summary>The vertical field of view.</summary>
    public required double FieldOfView { get; init; }

    /// <summary>The chosen user interface size setting.</summary>
    public required UiSize UiSize { get; init; }
}
