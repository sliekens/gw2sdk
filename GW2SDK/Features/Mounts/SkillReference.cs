namespace GuildWars2.Mounts;

[PublicAPI]
[DataTransferObject]
public sealed record SkillReference
{
    public required int Id { get; init; }

    public required SkillSlot Slot { get; init; }
}
