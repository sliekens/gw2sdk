namespace GuildWars2.Hero.Equipment.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record SkillReference
{
    public required int Id { get; init; }

    public required SkillSlot Slot { get; init; }
}
