namespace GuildWars2.Mumble;

[PublicAPI]
[DataTransferObject]
public sealed record Identity
{
    public required string Name { get; init; }

    public required ProfessionName Profession { get; init; }

    public required int SpecializationId { get; init; }

    public required RaceName Race { get; init; }

    public required int MapId { get; init; }

    public required long WorldId { get; init; }

    public required int TeamColorId { get; init; }

    public required bool Commander { get; init; }

    /// <summary>The vertical field of view.</summary>
    public required double FieldOfView { get; init; }

    public required UiSize UiSize { get; init; }
}
