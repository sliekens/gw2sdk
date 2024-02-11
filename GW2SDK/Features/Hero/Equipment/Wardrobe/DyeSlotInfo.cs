namespace GuildWars2.Hero.Equipment.Wardrobe;

[PublicAPI]
[DataTransferObject]
public sealed record DyeSlotInfo
{
    public required IReadOnlyList<DyeSlot?> Default { get; init; }

    public required IReadOnlyList<DyeSlot?>? AsuraFemale { get; init; }

    public required IReadOnlyList<DyeSlot?>? AsuraMale { get; init; }

    public required IReadOnlyList<DyeSlot?>? CharrFemale { get; init; }

    public required IReadOnlyList<DyeSlot?>? CharrMale { get; init; }

    public required IReadOnlyList<DyeSlot?>? HumanFemale { get; init; }

    public required IReadOnlyList<DyeSlot?>? HumanMale { get; init; }

    public required IReadOnlyList<DyeSlot?>? NornFemale { get; init; }

    public required IReadOnlyList<DyeSlot?>? NornMale { get; init; }

    public required IReadOnlyList<DyeSlot?>? SylvariFemale { get; init; }

    public required IReadOnlyList<DyeSlot?>? SylvariMale { get; init; }
}
