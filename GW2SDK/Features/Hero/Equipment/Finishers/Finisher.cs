using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Finishers;

/// <summary>Information about a finisher.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(FinisherJsonConverter))]
public sealed record Finisher
{
    /// <summary>The finisher ID.</summary>
    public required int Id { get; init; }

    /// <summary>A description of how to obtain the finisher, as it appears in the tooltip of a locked finisher.</summary>
    public required string LockedText { get; init; }

    /// <summary>The IDs of the items that unlock the finisher when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>The display order of the finisher in the equipment panel.</summary>
    public required int Order { get; init; }

    /// <summary>The URL of the finisher icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The name of the finisher.</summary>
    public required string Name { get; init; }

    /// <inheritdoc />
    public bool Equals(Finisher? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id
            && LockedText == other.LockedText
            && UnlockItemIds.SequenceEqual(other.UnlockItemIds)
            && Order == other.Order
            && IconHref == other.IconHref
            && Name == other.Name;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, LockedText, UnlockItemIds, Order, IconHref, Name);
    }
}
