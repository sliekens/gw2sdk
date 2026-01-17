using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about the default dyes for a piece of armor.</summary>
[DataTransferObject]
[JsonConverter(typeof(DyeSlotInfoJsonConverter))]
public sealed record DyeSlotInfo
{
    /// <summary>The default dyes for the armor to use when there are no overrides.</summary>
    public required IImmutableValueList<DyeSlot?> Default { get; init; }

    /// <summary>The dye slots for Asuran females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? AsuraFemale { get; init; }

    /// <summary>The dye slots for Asuran males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? AsuraMale { get; init; }

    /// <summary>The dye slots for Charr females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? CharrFemale { get; init; }

    /// <summary>The dye slots for Charr males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? CharrMale { get; init; }

    /// <summary>The dye slots for Human females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? HumanFemale { get; init; }

    /// <summary>The dye slots for Human males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? HumanMale { get; init; }

    /// <summary>The dye slots for Norn females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? NornFemale { get; init; }

    /// <summary>The dye slots for Norn males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? NornMale { get; init; }

    /// <summary>The dye slots for Sylvari females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? SylvariFemale { get; init; }

    /// <summary>The dye slots for Sylvari males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IImmutableValueList<DyeSlot?>? SylvariMale { get; init; }
}
