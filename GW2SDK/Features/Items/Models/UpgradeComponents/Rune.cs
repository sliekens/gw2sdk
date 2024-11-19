using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a rune, which is used to upgrade armor.</summary>
[PublicAPI]
[JsonConverter(typeof(RuneJsonConverter))]
public sealed record Rune : UpgradeComponent
{
    /// <summary>The bonuses provided by this rune.</summary>
    /// <remarks>Each equipped rune gives one bonus from this list. The bonuses are cumulative and depend on the number of
    /// identical runes equipped. For example, if you have two runes of the same type equipped, you will get the first two
    /// bonuses from the list. If you have three of the same runes equipped, you will get the first three bonuses, and so on.</remarks>
    public required IReadOnlyList<string>? Bonuses { get; init; }

    /// <inheritdoc />
    public bool Equals(Rune? other)
    {
        return ReferenceEquals(this, other) || (base.Equals(other) && BonusesEqual(this, other));

        static bool BonusesEqual(Rune left, Rune right)
        {
            if (left.Bonuses is null && right.Bonuses is null)
            {
                return true;
            }

            if (left.Bonuses is null || right.Bonuses is null)
            {
                return false;
            }

            return left.Bonuses.SequenceEqual(right.Bonuses);
        }
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());

        foreach (var bonus in Bonuses ?? [])
        {
            hash.Add(bonus);
        }

        return hash.ToHashCode();
    }
}
