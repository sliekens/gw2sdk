using System;
using System.Text;

using JetBrains.Annotations;

namespace GW2SDK;

/// <summary>Represents an amount of coins and provides methods to compare and convert coins to other formats.</summary>
[PublicAPI]
public readonly struct Coin : IEquatable<Coin>, IComparable<Coin>, IComparable
{
    /// <summary>Gets the total value in copper coins.</summary>
    public readonly int Amount;

    public static readonly Coin Zero = 0;

    public Coin(
        int gold,
        int silver,
        int copper
    )
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
        if (amount < Zero.Amount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "The amount of coins cannot be negative.");
        }

        Amount = amount;
    }

    public bool Equals(Coin other)
    {
        return Amount == other.Amount;
    }

    public override bool Equals(object? obj)
    {
        return obj is Coin other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Amount;
    }

    public static bool operator ==(Coin left, Coin right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Coin left, Coin right)
    {
        return !left.Equals(right);
    }

    public int CompareTo(Coin other)
    {
        return Amount.CompareTo(other.Amount);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return 1;
        }

        return obj is Coin other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(Coin)}");
    }

    public static bool operator <(Coin left, Coin right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Coin left, Coin right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Coin left, Coin right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Coin left, Coin right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static Coin operator +(Coin coin)
    {
        return new Coin(coin.Amount);
    }

    public static Coin operator -(Coin coin)
    {
        return new Coin(-coin.Amount);
    }

    public static Coin operator ++(Coin coin)
    {
        return new Coin(coin.Amount + 1);
    }

    public static Coin operator --(Coin coin)
    {
        return new Coin(coin.Amount - 1);
    }

    public static Coin operator +(Coin left, Coin right)
    {
        return new Coin(left.Amount + right.Amount);
    }

    public static Coin operator -(Coin left, Coin right)
    {
        return new Coin(left.Amount - right.Amount);
    }

    public static Coin operator *(Coin left, Coin right)
    {
        return new Coin(left.Amount * right.Amount);
    }

    public static Coin operator /(Coin left, Coin right)
    {
        return new Coin(left.Amount / right.Amount);
    }

    public static Coin operator %(Coin left, Coin right)
    {
        return new Coin(left.Amount % right.Amount);
    }

    public static implicit operator int(Coin coin)
    {
        return coin.Amount;
    }

    public static implicit operator Coin(int quantity)
    {
        return new Coin(quantity);
    }

    public override string ToString()
    {
        StringBuilder str = new(32);
        var copper = Amount;
        var gold = copper / 1_00_00;
        copper %= 1_00_00;
        var silver = copper / 1_00;
        copper %= 1_00;
        if (gold != 0)
        {
            str.AppendFormat("{0:N0} gold", gold);
        }

        if (silver != 0)
        {
            if (str.Length != 0)
            {
                str.Append(", ");
            }

            str.AppendFormat("{0:N0} silver", silver);
        }

        if (copper != 0)
        {
            if (str.Length != 0)
            {
                str.Append(", ");
            }

            str.AppendFormat("{0:N0} copper", copper);
        }

        if (str.Length == 0)
        {
            str.Append("⸻");
        }

        return str.ToString();
    }
}