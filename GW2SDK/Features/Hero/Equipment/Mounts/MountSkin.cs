namespace GuildWars2.Hero.Equipment.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record MountSkin
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string IconHref { get; init; }

    public required IReadOnlyCollection<DyeSlot> DyeSlots { get; init; }

    public required MountName Mount { get; init; }
}
