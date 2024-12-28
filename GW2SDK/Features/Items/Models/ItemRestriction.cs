using System.Text.Json.Serialization;
using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>Information about which characters can use an item.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(ItemRestrictionJsonConverter))]
public sealed record ItemRestriction
{
    /// <summary>The race(s) that can use the item.</summary>
    public required IReadOnlyCollection<Extensible<RaceName>> Races { get; init; }

    /// <summary>The profession(s) that can use the item.</summary>
    public required IReadOnlyCollection<Extensible<ProfessionName>> Professions { get; init; }

    /// <summary>The body type(s) that can use the item.</summary>
    public required IReadOnlyCollection<Extensible<BodyType>> BodyTypes { get; init; }

    /// <summary>Other undocumented restrictions. If you find out what they mean, please open an issue or a pull request.</summary>
    public required IReadOnlyList<string> Other { get; init; }

    /// <inheritdoc />
    public bool Equals(ItemRestriction? other)
    {
        if (ReferenceEquals(this, other)) return true;
        if (other is null) return false;

        return Races.SequenceEqual(other.Races)
            && Professions.SequenceEqual(other.Professions)
            && BodyTypes.SequenceEqual(other.BodyTypes)
            && Other.SequenceEqual(other.Other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var race in Races)
        {
            hash.Add(race);
        }

        foreach (var profession in Professions)
        {
            hash.Add(profession);
        }

        foreach (var bodyType in BodyTypes)
        {
            hash.Add(bodyType);
        }

        foreach (var item in Other)
        {
            hash.Add(item);
        }

        return hash.ToHashCode();
    }
}
