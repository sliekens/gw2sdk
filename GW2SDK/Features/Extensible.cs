﻿using System.Diagnostics;
using System.Text.Json.Serialization;

namespace GuildWars2;

/// <summary>Represents an enum that can be extended with additional values.</summary>
/// <typeparam name="TEnum">The type of the enum.</typeparam>
/// <param name="Name">The name of the enum value.</param>
[PublicAPI]
[DebuggerDisplay("{ToString(),nq}")]
[JsonConverter(typeof(ExtensibleEnumJsonConverterFactory))]
public struct Extensible<TEnum>(string Name)
    : IComparable<Extensible<TEnum>>, IComparable, IEquatable<Extensible<TEnum>>
    where TEnum : struct, Enum
{
    /// <summary>Determines whether the current name is defined in the enum.</summary>
    /// <returns><c>true</c> if the name is defined in the enum; otherwise, <c>false</c>.</returns>
    public bool IsDefined()
    {
        if (Name is null)
        {
            return Enum.IsDefined(typeof(TEnum), 0);
        }

        return Enum.TryParse<TEnum>(Name, true, out _);
    }

    /// <summary>Converts the current name to the corresponding enum value.</summary>
    /// <value>The corresponding enum value if the conversion is successful; otherwise, <c>null</c>.</value>
    public TEnum? ToEnum()
    {
        if (Name is null)
        {
            return Enum.IsDefined(typeof(TEnum), 0) ? default(TEnum) : null;
        }

        if (Enum.TryParse<TEnum>(Name, true, out var value))
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
    public override string ToString()
    {
        if (Name is not null)
        {
            return Name;
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
    public static bool operator ==(Extensible<TEnum> left, Extensible<TEnum> right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Extensible<TEnum> left, Extensible<TEnum> right)
    {
        return !left.Equals(right);
    }

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Extensible<TEnum> left, TEnum right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Extensible<TEnum> left, TEnum right)
    {
        return !left.Equals(right);
    }

    /// <summary>Determines whether two enums are equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Extensible<TEnum> left, string right)
    {
        return left.Equals(right);
    }

    /// <summary>Determines whether two enums are not equal.</summary>
    /// <param name="left">The first Extensible object to compare.</param>
    /// <param name="right">The second Extensible object to compare.</param>
    /// <returns><c>true</c> if the two objects are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Extensible<TEnum> left, string right)
    {
        return !left.Equals(right);
    }

    /// <summary>Implicitly converts a string to an instance of the Extensible enum class.</summary>
    /// <param name="name">The name of the enum value.</param>
    /// <returns>An instance of the Extensible enum class.</returns>
    public static implicit operator Extensible<TEnum>(string name)
    {
        return new(name);
    }

    /// <summary>Implicitly converts an enum to an instance of the Extensible enum class.</summary>
    /// <param name="name">The enum value.</param>
    /// <returns>An instance of the Extensible enum class.</returns>
    public static implicit operator Extensible<TEnum>(TEnum name)
    {
        return new(name.ToString());
    }
}
