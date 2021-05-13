using System;
using GW2SDK.Annotations;
using static System.String;

namespace GW2SDK
{
    [PublicAPI]
    public sealed class SchemaVersion : IComparable
    {
        public static readonly SchemaVersion Latest = new("latest");

        public static readonly SchemaVersion V20190221 = new("2019-02-21T00:00:00.000Z");

        public static readonly SchemaVersion V20190322 = new("2019-03-22T00:00:00.000Z");

        public static readonly SchemaVersion V20190516 = new("2019-05-16T00:00:00.000Z");

        public static readonly SchemaVersion V20190521 = new("2019-05-21T23:00:00.000Z");

        public static readonly SchemaVersion V20190522 = new("2019-05-22T00:00:00.000Z");

        public static readonly SchemaVersion V20191219 = new("2019-12-19T00:00:00.000Z");

        public static readonly SchemaVersion V20201117 = new("2020-11-17T00:30:00.000Z");

        private SchemaVersion(string version)
        {
            if (IsNullOrWhiteSpace(version))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(version));
            }

            Version = version;
        }

        public string Version { get; }

        public int CompareTo(object obj) => Compare(Version, ((SchemaVersion) obj).Version, StringComparison.Ordinal);

        public override bool Equals(object obj)
        {
            if (obj is not SchemaVersion other)
            {
                return false;
            }

            return Version == other.Version;
        }

        public override int GetHashCode() => Version.GetHashCode();

        public override string ToString() => Version;

        public static implicit operator string(SchemaVersion instance) => instance.Version;
    }
}
