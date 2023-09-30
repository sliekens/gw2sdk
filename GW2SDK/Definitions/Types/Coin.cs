using System.Text;

namespace GuildWars2;

/// <summary>Represents an amount of coins and provides methods to compare and convert coins to other formats.</summary>
[PublicAPI]
public readonly record struct Coin : IComparable, IComparable<Coin>
{
    /// <summary>Gets the total value in copper coins.</summary>
    public readonly int Amount;

    public static readonly Coin Zero = 0;

    public Coin(int gold, int silver, int copper)
        : this((10_000 * gold) + (100 * silver) + copper)
    {
    }

    public Coin(int silver, int copper)
        : this((100 * silver) + copper)
    {
    }

    /// <summary>Creates a Coin value from the specified amount of copper coins.</summary>
    /// <param name="amount">The total number of copper coins.</param>
    public Coin(int amount)
    {
        Amount = amount;
    }

    public int CompareTo(Coin other) => Amount.CompareTo(other.Amount);

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return 1;
        }

        return obj is Coin other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(Coin)}", nameof(obj));
    }

    public static bool operator <(Coin left, Coin right) => left.CompareTo(right) < 0;

    public static bool operator >(Coin left, Coin right) => left.CompareTo(right) > 0;

    public static bool operator <=(Coin left, Coin right) => left.CompareTo(right) <= 0;

    public static bool operator >=(Coin left, Coin right) => left.CompareTo(right) >= 0;

    public static Coin operator +(Coin coin) => new(coin.Amount);

    public static Coin operator -(Coin coin) => new(-coin.Amount);

    public static Coin operator ++(Coin coin) => new(coin.Amount + 1);

    public static Coin operator --(Coin coin) => new(coin.Amount - 1);

    public static Coin operator +(Coin left, Coin right) => new(left.Amount + right.Amount);

    public static Coin operator -(Coin left, Coin right) => new(left.Amount - right.Amount);

    public static Coin operator *(Coin left, Coin right) => new(left.Amount * right.Amount);

    public static Coin operator /(Coin left, Coin right) => new(left.Amount / right.Amount);

    public static Coin operator %(Coin left, Coin right) => new(left.Amount % right.Amount);

    public static implicit operator int(Coin coin) => coin.Amount;

    public static implicit operator Coin(int quantity) => new(quantity);

    public override string ToString()
    {
        if (Amount == 0)
        {
            return "⸻";
        }

        var copper = Amount;
        var gold = copper / 1_00_00;
        copper %= 1_00_00;
        var silver = copper / 1_00;
        copper %= 1_00;

        string? formattedGold = null;
        string? formattedSilver = null;
        string? formattedCopper = null;
        int length = 0;
        if (gold != 0)
        {
            formattedGold = $"{gold:N0} gold";
            length += formattedGold.Length;
        }

        if (silver != 0)
        {
            if (length > 0)
            {
                length += 2;
            }

            formattedSilver = $"{silver:N0} silver";
            length += formattedSilver.Length;
        }

        if (copper != 0)
        {
            if (length > 0)
            {
                length += 2;
            }

            formattedCopper = $"{copper:N0} copper";
            length += formattedCopper.Length;
        }

#if NET
        return string.Create(length, (formattedGold, formattedSilver, formattedCopper), (buffer, state) =>
        {
            var pos = 0;

            if (state.formattedGold is not null)
            {
                state.formattedGold.AsSpan().CopyTo(buffer);
                pos += state.formattedGold.Length;
            }

            if (state.formattedSilver is not null)
            {
                if (pos > 0)
                {
                    buffer[pos++] = ',';
                    buffer[pos++] = ' ';
                }

                state.formattedSilver.AsSpan().CopyTo(buffer[pos..]);
                pos += state.formattedSilver.Length;
            }

            if (state.formattedCopper is not null)
            {
                if (pos > 0)
                {
                    buffer[pos++] = ',';
                    buffer[pos++] = ' ';
                }

                state.formattedCopper.AsSpan().CopyTo(buffer[pos..]);
            }
        });
#else
        var str = new StringBuilder(length);
        if (formattedGold is not null)
        {
            str.Append(formattedGold);
        }

        if (formattedSilver is not null)
        {
            if (str.Length != 0)
            {
                str.Append(", ");
            }

            str.Append(formattedSilver);
        }

        if (formattedCopper is not null)
        {
            if (str.Length != 0)
            {
                str.Append(", ");
            }

            str.Append(formattedCopper);
        }

        return str.ToString();
#endif
    }
}
