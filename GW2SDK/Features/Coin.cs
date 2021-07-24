using System;
using System.Text;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public readonly struct Coin : IEquatable<Coin>, IComparable<Coin>, IComparable
    {
        public readonly int Quantity;

        public static readonly Coin MinValue = 0;

        public Coin(int quantity)
        {
            if (quantity < MinValue.Quantity)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "The number of coins cannot be negative.");
            }

            Quantity = quantity;
        }

        public bool Equals(Coin other) => Quantity == other.Quantity;

        public override bool Equals(object? obj) => obj is Coin other && Equals(other);

        public override int GetHashCode() => Quantity;

        public static bool operator ==(Coin left, Coin right) => left.Equals(right);

        public static bool operator !=(Coin left, Coin right) => !left.Equals(right);

        public int CompareTo(Coin other) => Quantity.CompareTo(other.Quantity);

        public int CompareTo(object? obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            return obj is Coin other
                ? CompareTo(other)
                : throw new ArgumentException($"Object must be of type {nameof(Coin)}");
        }

        public static bool operator <(Coin left, Coin right) => left.CompareTo(right) < 0;

        public static bool operator >(Coin left, Coin right) => left.CompareTo(right) > 0;

        public static bool operator <=(Coin left, Coin right) => left.CompareTo(right) <= 0;

        public static bool operator >=(Coin left, Coin right) => left.CompareTo(right) >= 0;

        public static Coin operator +(Coin coin) => new (coin.Quantity);

        public static Coin operator -(Coin coin) => new (-coin.Quantity);

        public static Coin operator ++(Coin coin) => new (coin.Quantity + 1);

        public static Coin operator --(Coin coin) => new (coin.Quantity - 1);

        public static Coin operator +(Coin left, Coin right) => new(left.Quantity + right.Quantity);

        public static Coin operator -(Coin left, Coin right) => new(left.Quantity - right.Quantity);

        public static Coin operator *(Coin left, Coin right) => new(left.Quantity * right.Quantity);

        public static Coin operator /(Coin left, Coin right) => new(left.Quantity / right.Quantity);

        public static Coin operator %(Coin left, Coin right) => new(left.Quantity % right.Quantity);

        public static implicit operator int(Coin coin) => coin.Quantity;

        public static implicit operator Coin(int quantity) => new(quantity);

        public override string ToString()
        {
            var str = new StringBuilder(32);
            var copper = Quantity;
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
                str.Append("-");
            }

            return str.ToString();
        }
    }
}
