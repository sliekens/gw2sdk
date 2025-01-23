using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about the default dyes for a piece of armor.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(DyeSlotInfoJsonConverter))]
public sealed record DyeSlotInfo
{
    /// <summary>The default dyes for the armor to use when there are no overrides.</summary>
    public required IReadOnlyList<DyeSlot?> Default { get; init; }

    /// <summary>The dye slots for Asuran females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? AsuraFemale { get; init; }

    /// <summary>The dye slots for Asuran males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? AsuraMale { get; init; }

    /// <summary>The dye slots for Charr females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? CharrFemale { get; init; }

    /// <summary>The dye slots for Charr males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? CharrMale { get; init; }

    /// <summary>The dye slots for Human females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? HumanFemale { get; init; }

    /// <summary>The dye slots for Human males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? HumanMale { get; init; }

    /// <summary>The dye slots for Norn females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? NornFemale { get; init; }

    /// <summary>The dye slots for Norn males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? NornMale { get; init; }

    /// <summary>The dye slots for Sylvari females, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? SylvariFemale { get; init; }

    /// <summary>The dye slots for Sylvari males, which override the <see cref="Default" /> if not <see langword="null" /> .</summary>
    public required IReadOnlyList<DyeSlot?>? SylvariMale { get; init; }

    /// <inheritdoc />
    public bool Equals(DyeSlotInfo? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Default.SequenceEqual(other.Default)
            && (AsuraFemale is null
                ? other.AsuraFemale is null
                : other.AsuraFemale is not null && AsuraFemale.SequenceEqual(other.AsuraFemale))
            && (AsuraMale is null
                ? other.AsuraMale is null
                : other.AsuraMale is not null && AsuraMale.SequenceEqual(other.AsuraMale))
            && (CharrFemale is null
                ? other.CharrFemale is null
                : other.CharrFemale is not null && CharrFemale.SequenceEqual(other.CharrFemale))
            && (CharrMale is null
                ? other.CharrMale is null
                : other.CharrMale is not null && CharrMale.SequenceEqual(other.CharrMale))
            && (HumanFemale is null
                ? other.HumanFemale is null
                : other.HumanFemale is not null && HumanFemale.SequenceEqual(other.HumanFemale))
            && (HumanMale is null
                ? other.HumanMale is null
                : other.HumanMale is not null && HumanMale.SequenceEqual(other.HumanMale))
            && (NornFemale is null
                ? other.NornFemale is null
                : other.NornFemale is not null && NornFemale.SequenceEqual(other.NornFemale))
            && (NornMale is null
                ? other.NornMale is null
                : other.NornMale is not null && NornMale.SequenceEqual(other.NornMale))
            && (SylvariFemale is null
                ? other.SylvariFemale is null
                : other.SylvariFemale is not null
                && SylvariFemale.SequenceEqual(other.SylvariFemale))
            && (SylvariMale is null
                ? other.SylvariMale is null
                : other.SylvariMale is not null && SylvariMale.SequenceEqual(other.SylvariMale));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        foreach (var slot in Default)
        {
            hashCode.Add(slot);
        }

        foreach (var slot in AsuraFemale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in AsuraMale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in CharrFemale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in CharrMale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in HumanFemale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in HumanMale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in NornFemale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in NornMale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in SylvariFemale ?? [])
        {
            hashCode.Add(slot);
        }

        foreach (var slot in SylvariMale ?? [])
        {
            hashCode.Add(slot);
        }

        return hashCode.ToHashCode();
    }
}
