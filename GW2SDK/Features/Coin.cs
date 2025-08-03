using System.Text.Json.Serialization;

using GuildWars2.Chat;

#if !NET
using System.Text;
#endif

namespace GuildWars2;

/// <summary>Represents an amount of coins and provides methods to compare and convert coins to other formats.</summary>
[PublicAPI]
[JsonConverter(typeof(CoinJsonConverter))]
public readonly record struct Coin : IComparable, IComparable<Coin>
{
    /// <summary>Represents zero coins.</summary>
    public static readonly Coin Zero = 0;

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

    /// <summary>Gets the total value in copper coins.</summary>
    public int Amount { get; }

    /// <summary>The amount of gold coins.</summary>
    public int Gold => Amount / 1_00_00;

    /// <summary>The amount of silver coins.</summary>
    public int Silver => Amount % 1_00_00 / 1_00;

    /// <summary>The amount of copper coins.</summary>
    public int Copper => Amount % 1_00;

    /// <summary>Compares the current instance with another object.</summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>A value indicating the relative order of the objects being compared.</returns>
    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        if (obj is not Coin other)
        {
            ThrowHelper.ThrowBadArgument($"Object must be of type {nameof(Coin)}", nameof(obj));
            other = default;
        }

        return CompareTo(other);
    }

    /// <summary>Compares the current instance with another <see cref="Coin" /> value.</summary>
    /// <param name="other">The <see cref="Coin" /> value to compare with.</param>
    /// <returns>A value indicating the relative order of the objects being compared.</returns>
    public int CompareTo(Coin other)
    {
        return Amount.CompareTo(other.Amount);
    }

    /// <summary>Determines whether one <see cref="Coin" /> value is less than another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is less than <paramref name="right" />; otherwise, <c>false</c>.</returns>
    public static bool operator <(in Coin left, in Coin right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>Determines whether one <see cref="Coin" /> value is greater than another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />; otherwise, <c>false</c>.</returns>
    public static bool operator >(in Coin left, in Coin right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>Determines whether one <see cref="Coin" /> value is less than or equal to another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />; otherwise,
    /// <c>false</c>.</returns>
    public static bool operator <=(in Coin left, in Coin right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>Determines whether one <see cref="Coin" /> value is greater than or equal to another.</summary>
    /// <param name="left">The first <see cref="Coin" /> value to compare.</param>
    /// <param name="right">The second <see cref="Coin" /> value to compare.</param>
    /// <returns><c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />; otherwise,
    /// <c>false</c>.</returns>
    public static bool operator >=(in Coin left, in Coin right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>Returns the current instance of <see cref="Coin" />.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The current instance of <see cref="Coin" />.</returns>
    public static Coin operator +(in Coin coin)
    {
        return new(coin.Amount);
    }

    /// <summary>Returns the current instance of <see cref="Coin" />.</summary>
    /// <returns>The current instance of <see cref="Coin" />.</returns>
    public Coin Plus()
    {
        return new(Amount);
    }

    /// <summary>Returns the negation of the current instance of <see cref="Coin" />.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The negation of the current instance of <see cref="Coin" />.</returns>
    public static Coin operator -(in Coin coin)
    {
        return new(-coin.Amount);
    }

    /// <summary>Returns the negation of the current instance of <see cref="Coin" />.</summary>
    /// <returns>The negated <see cref="Coin" /> value.</returns>
    public Coin Negate()
    {
        return new(-Amount);
    }

    /// <summary>Increments the current instance of <see cref="Coin" /> by 1.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The incremented <see cref="Coin" /> value.</returns>
    public static Coin operator ++(in Coin coin)
    {
        return new(coin.Amount + 1);
    }

    /// <summary>Returns a new <see cref="Coin"/> value incremented by 1.</summary>
    /// <returns>The incremented <see cref="Coin"/> value.</returns>
    public Coin Increment()
    {
        return new(Amount + 1);
    }

    /// <summary>Decrements the current instance of <see cref="Coin" /> by 1.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The decremented <see cref="Coin" /> value.</returns>
    public static Coin operator --(in Coin coin)
    {
        return new(coin.Amount - 1);
    }

    /// <summary>Returns a new <see cref="Coin"/> value decremented by 1.</summary>
    /// <returns>The decremented <see cref="Coin"/> value.</returns>
    public Coin Decrement()
    {
        return new(Amount - 1);
    }

    /// <summary>Adds two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The sum of the two <see cref="Coin" /> values.</returns>
    public static Coin operator +(in Coin left, in Coin right)
    {
        return new(left.Amount + right.Amount);
    }

    /// <summary>Returns the sum of this <see cref="Coin"/> and another <see cref="Coin"/>.</summary>
    /// <param name="other">The <see cref="Coin"/> to add.</param>
    /// <returns>The sum of the two <see cref="Coin"/> values.</returns>
    public Coin Add(in Coin other)
    {
        return new(Amount + other.Amount);
    }

    /// <summary>Subtracts two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The difference between the two <see cref="Coin" /> values.</returns>
    public static Coin operator -(in Coin left, in Coin right)
    {
        return new(left.Amount - right.Amount);
    }

    /// <summary>Returns the difference between this <see cref="Coin"/> and another <see cref="Coin"/>.</summary>
    /// <param name="other">The <see cref="Coin"/> to subtract.</param>
    /// <returns>The difference between the two <see cref="Coin"/> values.</returns>
    public Coin Subtract(in Coin other)
    {
        return new(Amount - other.Amount);
    }

    /// <summary>Multiplies two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The product of the two <see cref="Coin" /> values.</returns>
    public static Coin operator *(in Coin left, in Coin right)
    {
        return new(left.Amount * right.Amount);
    }

    /// <summary>Returns the product of this <see cref="Coin"/> and another <see cref="Coin"/>.</summary>
    /// <param name="other">The <see cref="Coin"/> to multiply by.</param>
    /// <returns>The product of the two <see cref="Coin"/> values.</returns>
    public Coin Multiply(in Coin other)
    {
        return new(Amount * other.Amount);
    }

    /// <summary>Divides two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The quotient of the two <see cref="Coin" /> values.</returns>
    public static Coin operator /(in Coin left, in Coin right)
    {
        return new(left.Amount / right.Amount);
    }

    /// <summary>Returns the quotient of this <see cref="Coin"/> and another <see cref="Coin"/>.</summary>
    /// <param name="other">The <see cref="Coin"/> to divide by.</param>
    /// <returns>The quotient of the two <see cref="Coin"/> values.</returns>
    public Coin Divide(in Coin other)
    {
        return new(Amount / other.Amount);
    }

    /// <summary>Computes the remainder of dividing two <see cref="Coin" /> values.</summary>
    /// <param name="left">The first <see cref="Coin" /> value.</param>
    /// <param name="right">The second <see cref="Coin" /> value.</param>
    /// <returns>The remainder of dividing the two <see cref="Coin" /> values.</returns>
    public static Coin operator %(in Coin left, in Coin right)
    {
        return new(left.Amount % right.Amount);
    }

    /// <summary>Returns the remainder of dividing this <see cref="Coin"/> by another <see cref="Coin"/>.</summary>
    /// <param name="other">The <see cref="Coin"/> to divide by.</param>
    /// <returns>The remainder of dividing the two <see cref="Coin"/> values.</returns>
    public Coin Remainder(in Coin other)
    {
        return new(Amount % other.Amount);
    }

    /// <summary>Implicitly converts a <see cref="Coin" /> value to an <see cref="int" /> value.</summary>
    /// <param name="coin">The <see cref="Coin" /> value.</param>
    /// <returns>The <see cref="int" /> value representing the amount of coins.</returns>
    public static implicit operator int(in Coin coin)
    {
        return coin.Amount;
    }

    /// <summary>Converts this <see cref="Coin"/> value to an <see cref="int"/> that represents the amount of coins.</summary>
    /// <returns>The <see cref="int"/> value representing the amount of coins.</returns>
    public int ToInt32()
    {
        return Amount;
    }

    /// <summary>Implicitly converts an <see cref="int" /> value to a <see cref="Coin" /> value.</summary>
    /// <param name="quantity">The <see cref="int" /> value representing the amount of coins.</param>
    /// <returns>The <see cref="Coin" /> value.</returns>
    public static implicit operator Coin(int quantity)
    {
        return new(quantity);
    }

    /// <summary>Creates a <see cref="Coin"/> value from an <see cref="int"/> value.</summary>
    /// <param name="quantity">The <see cref="int"/> value representing the amount of coins.</param>
    /// <returns>The <see cref="Coin"/> value.</returns>
    public static Coin FromInt32(int quantity)
    {
        return new(quantity);
    }

#pragma warning disable CA1024 // Use properties where appropriate
    /// <summary>Gets the chat link representation of the current instance of <see cref="Coin" />.</summary>
    /// <returns>The chat link representation of the current instance of <see cref="Coin" />.</returns>
    public CoinLink GetChatLink()
    {
        return new() { Coins = this };
    }
#pragma warning restore CA1024 // Use properties where appropriate

    /// <summary>Gets the string representation of the <see cref="Coin" /> value.</summary>
    /// <returns>Returns the current <see cref="Coin" /> value formatted for display, (for example) 12 gold, 34 silver, 56
    /// copper.</returns>
    public override string ToString()
    {
        if (Amount == 0)
        {
            return "⸻";
        }

        string? formattedGold = null;
        string? formattedSilver = null;
        string? formattedCopper = null;
        int length = 0;
        if (Gold != 0)
        {
            formattedGold = $"{Gold:N0} gold";
            length += formattedGold.Length;
        }

        if (Silver != 0)
        {
            if (length > 0)
            {
                length += 2;
            }

            formattedSilver = $"{Silver:N0} silver";
            length += formattedSilver.Length;
        }

        if (Copper != 0)
        {
            if (length > 0)
            {
                length += 2;
            }

            formattedCopper = $"{Copper:N0} copper";
            length += formattedCopper.Length;
        }

#if NET
        return string.Create(
            length,
            (formattedGold, formattedSilver, formattedCopper),
            (buffer, state) =>
            {
                int pos = 0;

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
        StringBuilder str = new(length);
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
