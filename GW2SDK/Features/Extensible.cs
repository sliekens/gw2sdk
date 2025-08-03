using System.Diagnostics;
using System.Text.Json.Serialization;

namespace GuildWars2;

/// <summary>Represents an enum that can be extended with additional values.</summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
/// <param name="name">The name of the enum value.</param>
[PublicAPI]
[DebuggerDisplay("{ToString(),nq}")]
[JsonConverter(typeof(ExtensibleEnumJsonConverterFactory))]
public readonly struct Extensible<TEnum>(string name)
    : IComparable<Extensible<TEnum>>, IComparable, IEquatable<Extensible<TEnum>>
    where TEnum : struct, Enum
{
    /// <summary>Determines whether the current name is defined in the enum.</summary>
    /// <returns><c>true</c> if the name is defined in the enum; otherwise, <c>false</c>.</returns>
    public bool IsDefined()
    {
        if (name is null)
        {
            return Enum.IsDefined(typeof(TEnum), 0);
        }

        return Enum.TryParse<TEnum>(name, true, out _);
    }

    /// <summary>Converts the current name to the corresponding enum value.</summary>
    /// <value>The corresponding enum value if the conversion is successful; otherwise, <c>null</c>.</value>
    public TEnum? ToEnum()
    {
        if (name is null)
        {
            return Enum.IsDefined(typeof(TEnum), 0) ? default(TEnum) : null;
        }

        if (Enum.TryParse<TEnum>(name, true, out TEnum value))
        {
            return value;
        }

        return null;
    }

    /// <summary>Determines whether the current instance of Extensible is equal to another Extensible object.</summary>
    /// <param name="other">The Extensible object to compare with the current instance.</param>
    /// <returns><c>true</c> if the current instance is equal to the other object; otherwise, <c>false</c>.</returns>
    public bool Equals(Extensible<TEnum> other)
    {
        return string.Equals(ToString(), other.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public int CompareTo(Extensible<TEnum> other)
    {
        return string.Compare(ToString(), other.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        return obj is Extensible<TEnum> other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(Extensible<TEnum>)}");
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Extensible<TEnum> other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return StringComparer.OrdinalIgnoreCase.GetHashCode(ToString());
    }

    /// <inheritdoc />
    public override readonly string ToString()
    {
        if (name is not null)
        {
            return name;
        }

        if (Enum.IsDefined(typeof(TEnum), 0))
        {
            return default(TEnum)!.ToString();
        }

        return "";
    }

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(in Extensible<TEnum> left, in Extensible<TEnum> right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(in Extensible<TEnum> left, in Extensible<TEnum> right)
    {
        return !left.Equals(right);
    }

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(in Extensible<TEnum> left, TEnum right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(in Extensible<TEnum> left, TEnum right)
    {
        return !left.Equals(right);
    }

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(in Extensible<TEnum> left, string right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(in Extensible<TEnum> left, string right)
    {
        return !left.Equals(right);
    }

    /// <summary>Determines whether one Extensible object is less than another.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if left is less than right; otherwise, <c>false</c>.</returns>
    public static bool operator <(in Extensible<TEnum> left, in Extensible<TEnum> right)
    {
        return left.CompareTo(right) < 0;
    }

    /// <summary>Determines whether one Extensible object is less than or equal to another.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if left is less than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator <=(in Extensible<TEnum> left, in Extensible<TEnum> right)
    {
        return left.CompareTo(right) <= 0;
    }

    /// <summary>Determines whether one Extensible object is greater than another.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if left is greater than right; otherwise, <c>false</c>.</returns>
    public static bool operator >(in Extensible<TEnum> left, in Extensible<TEnum> right)
    {
        return left.CompareTo(right) > 0;
    }

    /// <summary>Determines whether one Extensible object is greater than or equal to another.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if left is greater than or equal to right; otherwise, <c>false</c>.</returns>
    public static bool operator >=(in Extensible<TEnum> left, in Extensible<TEnum> right)
    {
        return left.CompareTo(right) >= 0;
    }

    /// <summary>Implicitly converts a string to an instance of the Extensible enum class.</summary>
    /// <param name="name">The name of the enum value.</param>
    /// <returns>An instance of the Extensible enum class.</returns>
    public static implicit operator Extensible<TEnum>(string name)
    {
        return new(name);
    }

#pragma warning disable CA1000 // Do not declare static members on generic types
    /// <summary>Converts a string to an instance of the Extensible enum class.</summary>
    /// <param name="name">The name of the enum value.</param>
    /// <returns>An instance of the Extensible enum class.</returns>
    public static Extensible<TEnum> FromString(string name)
    {
        return new(name);
    }
#pragma warning restore CA1000 // Do not declare static members on generic types

    /// <summary>Implicitly converts an enum to an instance of the Extensible enum class.</summary>
    /// <param name="name">The enum value.</param>
    /// <returns>An instance of the Extensible enum class.</returns>
    public static implicit operator Extensible<TEnum>(TEnum name)
    {
        return new(name.ToString());
    }

#pragma warning disable CA1000 // Do not declare static members on generic types
    /// <summary>Converts a value of type TEnum to an instance of Extensible&lt;TEnum&gt;.</summary>
    /// <param name="value">The enum value.</param>
    /// <returns>An instance of Extensible&lt;TEnum&gt;.</returns>
    public static Extensible<TEnum> ToExtensible(TEnum value)
    {
        return new(value.ToString());
    }
#pragma warning restore CA1000 // Do not declare static members on generic types
}
