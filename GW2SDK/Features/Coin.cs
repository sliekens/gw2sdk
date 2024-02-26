using GuildWars2.Chat;
#if !NET
using System.Text;
#endif

namespace GuildWars2;

/// <summary>Represents an amount of coins and provides methods to compare and convert coins to other formats.</summary>
[PublicAPI]
public readonly record struct Coin : IComparable, IComparable<Coin>
{
    /// <summary>Represents zero coins.</summary>
    public static readonly Coin Zero = 0;

    /// <summary>Gets the total value in copper coins.</summary>
    public readonly int Amount;

    /// <summary>Initializes a new instance of the <see cref="Coin" /> struct with the specified amount of gold, silver, and
    /// copper coins.</summary>
    /// <param name="gold">The amount of gold coins.</param>
    /// <param name="silver">The amount of silver coins.</param>
    /// <param name="copper">The amount of copper coins.</param>
    public Coin(int gold, int silver, int copper)
        : this((10_000 * gold) + (100 * silver) + copper)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Coin" /> struct with the specified amount of silver and copper
    /// coins.</summary>
    /// <param name="silver">The amount of silver coins.</param>
    /// <param name="copper">The amount of copper coins.</param>
    public Coin(int silver, int copper)
        : this((100 * silver) + copper)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="Coin" /> struct with the specified amount of copper coins.</summary>
    /// <param name="amount">The total number of copper coins.</param>
    public Coin(int amount)
    {
        Amount = amount;
    }

    /// <summary>Compares the current instance with another object.</summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>A value indicating the relative order of the objects being compared.</returns>
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

    /// <summary>Compares the current instance with another <see cref="Coin" /> value.</summary>
    /// <param name="other">The <see cref="Coin" /> value to compare with.</param>
    /// <returns>A value indicating the relative order of the objects being compared.</returns>
    public int CompareTo(Coin other) => Amount.CompareTo(other.Amount);

    /// <summary>Determines whether one <see cref="Coin" /> value is less than another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <c>false</c>.</returns>
    public static bool operator <(Coin left, Coin right) => left.CompareTo(right) < 0;

    /// <summary>Determines whether one <see cref="Coin" /> value is greater than another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <c>false</c>.</returns>
    public static bool operator >(Coin left, Coin right) => left.CompareTo(right) > 0;

    /// <summary>Determines whether one <see cref="Coin" /> value is less than or equal to another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise,
    /// <c>false</c>.</returns>
    public static bool operator <=(Coin left, Coin right) => left.CompareTo(right) <= 0;

    /// <summary>Determines whether one <see cref="Coin" /> value is greater than or equal to another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise,
    /// <c>false</c>.</returns>
    public static bool operator >=(Coin left, Coin right) => left.CompareTo(right) >= 0;

    /// <summary>Returns the current instance of <see cref="Coin" />.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The current instance of <see cref="Coin" />.</returns>
    public static Coin operator +(Coin coin) => new(coin.Amount);

    /// <summary>Returns the negation of the current instance of <see cref="Coin" />.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The negation of the current instance of <see cref="Coin" />.</returns>
    public static Coin operator -(Coin coin) => new(-coin.Amount);

    /// <summary>Increments the current instance of <see cref="Coin" /> by 1.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The incremented <see cref="Coin" /> value.</returns>
    public static Coin operator ++(Coin coin) => new(coin.Amount + 1);

    /// <summary>Decrements the current instance of <see cref="Coin" /> by 1.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The decremented <see cref="Coin" /> value.</returns>
    public static Coin operator --(Coin coin) => new(coin.Amount - 1);

    /// <summary>Adds two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The sum of the two <see cref="Coin" /> values.</returns>
    public static Coin operator +(Coin left, Coin right) => new(left.Amount + right.Amount);

    /// <summary>Subtracts two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The difference between the two <see cref="Coin" /> values.</returns>
    public static Coin operator -(Coin left, Coin right) => new(left.Amount - right.Amount);

    /// <summary>Multiplies two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The product of the two <see cref="Coin" /> values.</returns>
    public static Coin operator *(Coin left, Coin right) => new(left.Amount * right.Amount);

    /// <summary>Divides two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The quotient of the two <see cref="Coin" /> values.</returns>
    public static Coin operator /(Coin left, Coin right) => new(left.Amount / right.Amount);

    /// <summary>Computes the remainder of dividing two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The remainder of dividing the two <see cref="Coin" /> values.</returns>
    public static Coin operator %(Coin left, Coin right) => new(left.Amount % right.Amount);

    /// <summary>Implicitly converts a <see cref="Coin" /> value to an <see cref="int" /> value.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The <see cref="int" /> value representing the amount of coins.</returns>
    public static implicit operator int(Coin coin) => coin.Amount;

    /// <summary>Implicitly converts an <see cref="int" /> value to a <see cref="Coin" /> value.</summary>
    /// <param name="quantity">The <see cref="int" /> value representing the amount of coins.</param>
    /// <returns>The <see cref="Coin" /> value.</returns>
    public static implicit operator Coin(int quantity) => new(quantity);

    /// <summary>Gets the chat link representation of the current instance of <see cref="Coin" />.</summary>
    /// <returns>The chat link representation of the current instance of <see cref="Coin" />.</returns>
    public CoinLink GetChatLink() => new() { Coins = this };

    /// <summary>Gets the string representation of the <see cref="Coin" /> value.</summary>
    /// <returns>Returns the current <see cref="Coin" /> value formatted for display, (for example) 12 gold, 34 silver, 56 copper.</returns>
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
        var length = 0;
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
        return string.Create(
            length,
            (formattedGold, formattedSilver, formattedCopper),
            (buffer, state) =>
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
            }
        );
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
