using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public readonly struct Coin : IEquatable<Coin>, IComparable<Coin>, IComparable
    {
        public readonly int Quantity;

        public Coin(int quantity)
        {
            Quantity = quantity;
        }

        public bool Equals(Coin other) => Quantity == other.Quantity;

        public override bool Equals(object? obj) => obj is Coin other && Equals(other);

        public override int GetHashCode() => Quantity;

        public static bool operator ==(Coin left, Coin right) => left.Equals(right);

        public static bool operator !=(Coin left, Coin right) => !left.Equals(right);

        private sealed class CoinEqualityComparison : IEqualityComparer<Coin>
        {
            public bool Equals(Coin x, Coin y) => x.Quantity == y.Quantity;

            public int GetHashCode(Coin obj) => obj.Quantity;
        }

        public static IEqualityComparer<Coin> CoinEqualityComparer { get; } = new CoinEqualityComparison();

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

        private sealed class CoinRelationalComparison : IComparer<Coin>
        {
            public int Compare(Coin x, Coin y) => x.Quantity.CompareTo(y.Quantity);
        }

        public static IComparer<Coin> QualityComparer { get; } = new CoinRelationalComparison();

        public static implicit operator int(Coin coin) => coin.Quantity;

        public static implicit operator Coin(int quantity) => new(quantity);

        public override string ToString()
        {
            var str = new StringBuilder(15);
            var copper = Quantity;
            var gold = copper / 1_00_00;
            copper %= 1_00_00;
            var silver = copper / 1_00;
            copper %= 1_00;
            if (gold != 0)
            {
                str.AppendFormat("{0:N0}g", gold);
            }

            if (silver != 0)
            {
                if (str.Length != 0)
                {
                    str.Append(" ");
                }

                str.AppendFormat("{0:N0}s", silver);
            }

            if (copper != 0)
            {
                if (str.Length != 0)
                {
                    str.Append(" ");
                }

                str.AppendFormat("{0:N0}c", copper);
            }

            if (str.Length == 0)
            {
                str.Append("0");
            }

            return str.ToString();
        }
    }
}
